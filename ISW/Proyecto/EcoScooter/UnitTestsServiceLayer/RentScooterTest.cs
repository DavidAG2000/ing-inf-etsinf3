using EcoScooter.Entities;
using EcoScooter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestServiceEcoScooter
{
    [TestClass]
    public class RentScooterTest : BaseTest
    {
        //Scooter Data
        private static DateTime EXPECTED_REGISTER_DATE = DateTime.Now.AddYears(-1);


        //Station data
        private const string EXPECTED_ADDRESS = "Camino de Vera s/n. 46022 Valencia";
        private const string EXPECTED_ID = "UPV";
        private const double EXPECTED_LAT = 39.482369;
        private const double EXPECTED_LONG = -0.343578;
        //EmployeeData
        private static int TWENTY_AGE = 20;
        private static DateTime EXPECTED_BIRTHDATE = Date​Time.Now.AddYears(-TWENTY_AGE);
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
        private const double EXPECTED_FARE = 15;
        private const double EXPECTED_SPEED = 50;

        private static int LUSTRUM = 5;
        //User Data
        private static DateTime EXPECTED_USER_BIRTHDATE = Date​Time.Now.AddYears(-TWENTY_AGE + 1);
        private const string EXPECTED_USER_DNI = "29829859D";
        private const string EXPECTED_USER_EMAIL = "user@outlook.es";
        private const string EXPECTED_USER_NAME = "User Brown";
        private const int EXPECTED_USER_TELEPHON = 600331836;
        private const int EXPECTED_CVV = 123;
        private static DateTime EXPECTED_EXPIRATION_DATE = Date​Time.Now.AddYears(LUSTRUM);
        private const string EXPECTED_LOGIN = "user";
        private const int EXPECTED_NUMBER = 12345678;
        private const string EXPECTED_PASSWORD = "user_password";

        [TestMethod]
        public void NewRentalRightData()
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
            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            try
            {
                ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually
                ecoScooterService.RentScooter(EXPECTED_ID);
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect RentScooter service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void NewRentalRightDataPersits()
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
            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            try
            {
                ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually
                ecoScooterService.RentScooter(EXPECTED_ID);
                Rental rentalDAL = dal.GetAll<Rental>().First();
                Assert.IsNotNull(rentalDAL, "Rental is not stored in the database by RentScooter service");
                Assert.AreEqual(ScooterState.inUse, rentalDAL.Scooter.State, "The state of the Scooter is not updated in the database to inUse by RentScooter service");
                Assert.IsNull(rentalDAL.Scooter.Station, "Scooter wrong updated in the database by  RentScooter service. The station is not removed from the Scooter");
                Assert.IsFalse(rentalDAL.OriginStation.Scooters.Contains(rentalDAL.Scooter), "Station wrong updated in the database by  RentScooter service. The scooter still is in the station");
                Assert.IsTrue(rentalDAL.OriginStation.OriginRentals.Contains(rentalDAL), "Station wrong updated in the database by  RentScooter service. The rental is not in the originRentals collection ");
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect RentScooter service execution. Exception: " + e.Message);
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
            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RentScooter(EXPECTED_ID),
                "RentScooter doesn't control if there is an user is logged");

        }
        [TestMethod]
        public void BadStationId()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);

            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            User user = new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
               EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, ScooterState.available);

            ecoScooter.Scooters.Add(scooter);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RentScooter(EXPECTED_ID),
                "RentScooter doesn't control if the station id exists");

        }
        [TestMethod]
        public void NoScootersAtStation()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                     EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);

            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            User user = new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
               EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);

            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            ecoScooter.Stations.Add(station);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RentScooter(EXPECTED_ID),
                "RentScooter doesn't control if there are not scooters available");
        }
    }
}
