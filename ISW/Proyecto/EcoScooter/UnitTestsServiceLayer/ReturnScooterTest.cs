using EcoScooter.Entities;
using EcoScooter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestServiceEcoScooter
{
    [TestClass]
    public class ReturnScooterTest : BaseTest
    {  //Scooter Data
        private static DateTime EXPECTED_REGISTER_DATE = DateTime.Now.AddYears(-1);


        //Station data
        private const string EXPECTED_ADDRESS = "Camino de Vera s/n. 46022 Valencia";
        private const string EXPECTED_ID = "UPV";
        private const double EXPECTED_LAT = 39.482369;
        private const double EXPECTED_LONG = -0.343578;
        //EmployeeData
        private static int THIRTY_AGE = 30;
        private static DateTime EXPECTED_BIRTHDATE = Date​Time.Now.AddYears(-THIRTY_AGE);
        private const string EXPECTED_DNI = "14422178M";
        private const string EXPECTED_EMAIL = "somebody@outlook.es";
        private const string EXPECTED_NAME = "Somebody White";
        private const int EXPECTED_TELEPHON = 6006787;
        private const string EXPECTED_IBAN = "ES6621000418401234567891";
        private const int EXPECTED_PIN = 1234;
        private const string EXPECTED_POSITION = "manager";
        private const decimal EXPECTED_SALARY = 1250;
        //EcoScooterData
        private const double EXPECTED_DISCOUNT = 10;
        private const double EXPECTED_FARE = 0.15;
        private const double EXPECTED_SPEED = 50;

        private static int LUSTRUM = 5;
        //User Data
        private static DateTime EXPECTED_USER_BIRTHDATE = Date​Time.Now.AddYears(-THIRTY_AGE + 1);
        private const string EXPECTED_USER_DNI = "29829859D";
        private const string EXPECTED_USER_EMAIL = "user@outlook.es";
        private const string EXPECTED_USER_NAME = "User Brown";
        private const int EXPECTED_USER_TELEPHON = 600331836;
        private const int EXPECTED_CVV = 123;
        private static DateTime EXPECTED_EXPIRATION_DATE = Date​Time.Now.AddYears(LUSTRUM);
        private const string EXPECTED_LOGIN = "user";
        private const int EXPECTED_NUMBER = 12345678;
        private const string EXPECTED_PASSWORD = "user_password";
        //Rental data
        private const int ELLAPSED_MINUTES = 10;
        private static DateTime EXPECTED_START_DATE = Date​Time.Now.AddMinutes(-ELLAPSED_MINUTES);

        [TestMethod]
        public void NewReturnRightData()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                        EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);

            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            User user = new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
               EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, ScooterState.available);
            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            station.Scooters.Add(scooter);
            scooter.Station = station;

            ecoScooter.Scooters.Add(scooter);
            ecoScooter.Stations.Add(station);

            Rental rental = new Rental(null, 0, EXPECTED_START_DATE, station, scooter, user);
            user.Rentals.Add(rental);
            scooter.Rentals.Add(rental);
            station.OriginRentals.Add(rental);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            try
            {
                ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually
                ecoScooterService.ReturnScooter(EXPECTED_ID);
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect ReturnScooter service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void NewReturnRightDataPersist()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                        EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);

            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            User user = new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
               EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, ScooterState.available);
            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            station.Scooters.Add(scooter);
            scooter.Station = station;

            ecoScooter.Scooters.Add(scooter);
            ecoScooter.Stations.Add(station);

            Rental rental = new Rental(null, 0, EXPECTED_START_DATE, station, scooter, user);
            user.Rentals.Add(rental);
            scooter.Rentals.Add(rental);
            station.OriginRentals.Add(rental);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            try
            {
                ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually
                ecoScooterService.ReturnScooter(EXPECTED_ID);
                Rental rentalDAL = dal.GetAll<Rental>().First();
                Assert.IsNotNull(rentalDAL, "Rental is not stored in the database by ReturnScooter service");
                Assert.AreEqual(ScooterState.available, rentalDAL.Scooter.State, "The state of the Scooter is not updated in the database to available by ReturnScooter service");
                Assert.AreEqual(station.Id, rentalDAL.Scooter.Station.Id, "Scooter wrong updated in the database by  ReturnScooter service. The destination station is not added to the Scooter");
                Assert.IsTrue(rentalDAL.DestinationStation.DestinationRentals.Contains(rentalDAL), "Station wrong updated in the database by  ReturnScooter service. The rental is not in the DEstinationRentals collection ");
                Assert.IsNotNull(rentalDAL.EndDate, "Rental is not updated in the database by ReturnScooter service. EndDate still is null");
                Assert.AreEqual(ELLAPSED_MINUTES * EXPECTED_FARE, Convert.ToDouble(rentalDAL.Price), 0.1, "Price of the Rental bad calculated  by ReturnScooter service. ");
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect ReturnScooter service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void YoungReturnPersist()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                        EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);

            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);
            DateTime youngBirthDate = Date​Time.Now.AddYears(-THIRTY_AGE + 10);
            User user = new User(youngBirthDate, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
               EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, ScooterState.available);
            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            station.Scooters.Add(scooter);
            scooter.Station = station;

            ecoScooter.Scooters.Add(scooter);
            ecoScooter.Stations.Add(station);

            Rental rental = new Rental(null, 0, EXPECTED_START_DATE, station, scooter, user);
            user.Rentals.Add(rental);
            scooter.Rentals.Add(rental);
            station.OriginRentals.Add(rental);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            try
            {
                ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually
                ecoScooterService.ReturnScooter(EXPECTED_ID);
                Rental rentalDAL = dal.GetAll<Rental>().First();
                double expectedPrice = ELLAPSED_MINUTES * EXPECTED_FARE;
                Assert.AreEqual(expectedPrice - (expectedPrice * EXPECTED_DISCOUNT / 100), Convert.ToDouble(rentalDAL.Price), 0.1, "Price of the Rental bad calculated  by ReturnScooter service when user is under 25. ");
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect ReturnScooter service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void NotLoggedUser()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                         EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);

            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            User user = new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
               EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, ScooterState.available);
            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            station.Scooters.Add(scooter);
            scooter.Station = station;

            ecoScooter.Scooters.Add(scooter);
            ecoScooter.Stations.Add(station);

            Rental rental = new Rental(null, 0, EXPECTED_START_DATE, station, scooter, user);
            user.Rentals.Add(rental);
            scooter.Rentals.Add(rental);
            station.OriginRentals.Add(rental);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);

            Assert.ThrowsException<ServiceException>(() => ecoScooterService.ReturnScooter(EXPECTED_ID),
                "ReturnScooter doesn't control if there is an user is logged");

        }
        [TestMethod]
        public void NotExistsDestinationStation()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                         EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);

            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            User user = new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
               EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, ScooterState.available);
            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            station.Scooters.Add(scooter);
            scooter.Station = station;

            ecoScooter.Scooters.Add(scooter);
            ecoScooter.Stations.Add(station);

            Rental rental = new Rental(null, 0, EXPECTED_START_DATE, station, scooter, user);
            user.Rentals.Add(rental);
            scooter.Rentals.Add(rental);
            station.OriginRentals.Add(rental);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually

            Assert.ThrowsException<ServiceException>(() => ecoScooterService.ReturnScooter(""),
                "ReturnScooter doesn't control if the stationId doesn't exist");

        }
        [TestMethod]
        public void NotExistsPendingRental()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                         EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);

            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            User user = new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
               EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, ScooterState.available);
            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            station.Scooters.Add(scooter);
            scooter.Station = station;

            ecoScooter.Scooters.Add(scooter);
            ecoScooter.Stations.Add(station);

            Rental rental = new Rental(DateTime.Now, 0, EXPECTED_START_DATE, station, scooter, user);
            user.Rentals.Add(rental);
            scooter.Rentals.Add(rental);
            station.OriginRentals.Add(rental);
            station.DestinationRentals.Add(rental);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually

            Assert.ThrowsException<ServiceException>(() => ecoScooterService.ReturnScooter(EXPECTED_ID),
                "ReturnScooter doesn't control if tehre is not a pending rental");

        }
    }
}
