using EcoScooter.Entities;
using EcoScooter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestServiceEcoScooter
{
    [TestClass]
    public class GetRoutesTest : BaseTest
    {//Scooter Data
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
        private static DateTime EXPECTED_USER_BIRTHDATE = Date​Time.Now.AddYears(-TWENTY_AGE - LUSTRUM);
        private const string EXPECTED_USER_DNI = "29829859D";
        private const string EXPECTED_USER_EMAIL = "user@outlook.es";
        private const string EXPECTED_USER_NAME = "User Brown";
        private const int EXPECTED_USER_TELEPHON = 600331836;
        private const int EXPECTED_CVV = 123;
        private static DateTime EXPECTED_EXPIRATION_DATE = Date​Time.Now.AddYears(LUSTRUM);
        private const string EXPECTED_LOGIN = "user";
        private const int EXPECTED_NUMBER = 12345678;
        private const string EXPECTED_PASSWORD = "user_password";
        //Interval dates
        private const int TWO_WEEKS = 15;
        private static DateTime START_INTERVAL = DateTime.Now.AddMonths(-1);
        private static DateTime END_INTERVAL = DateTime.Now.AddDays(-TWO_WEEKS);


        private Rental InitDatabase()
        { //reutrn the id of the rentalin the inveral
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

            //In interval
            DateTime startDateIN = START_INTERVAL.AddDays(1);
            DateTime endDateIN = START_INTERVAL.AddDays(1).AddMinutes(LUSTRUM);
            decimal inPrice = Convert.ToDecimal(EXPECTED_FARE * LUSTRUM);
            Rental rentalIn = new Rental(endDateIN, inPrice, startDateIN, station, scooter, user);
            user.Rentals.Add(rentalIn);
            scooter.Rentals.Add(rentalIn);
            station.OriginRentals.Add(rentalIn);
            station.DestinationRentals.Add(rentalIn);

            //Out interval
            DateTime startDateOUT = END_INTERVAL.AddDays(1);
            DateTime endDateOUT = END_INTERVAL.AddDays(1).AddMinutes(LUSTRUM * 2);
            decimal outPrice = Convert.ToDecimal(EXPECTED_FARE * LUSTRUM * 2);
            Rental rentalOUT = new Rental(endDateOUT, outPrice, startDateOUT, station, scooter, user);
            user.Rentals.Add(rentalOUT);
            scooter.Rentals.Add(rentalOUT);
            station.OriginRentals.Add(rentalOUT);
            station.DestinationRentals.Add(rentalOUT);

            //ecoScooter has 1 employee, 1 user, 1 station , 1 scooter adn 2 rentals
            dal.Insert(ecoScooter);
            dal.Commit();
            return rentalIn;
        }
        [TestMethod]
        public void GetRoutesRightData()
        {
            InitDatabase();
            //Create the service
            //ecoScooter has 1 employee, 1 user, 1 station , 1 scooter and 2 rentals
            ecoScooterService = new EcoScooterService(dal);
            try
            {
                ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually
                ICollection<string> routesDescription = ecoScooterService.GetUserRoutes(START_INTERVAL, END_INTERVAL);
                decimal inPrice = Convert.ToDecimal(EXPECTED_FARE * LUSTRUM);
                Assert.IsTrue(routesDescription.Count == 1, "GetUserRoutes doesn't retrieve the rental in the interval");
                Assert.IsTrue(routesDescription.First().Split(',').Length == 5, "GetUserRoutes doesn't retrieve the rental in the required forma spplited by commas");
                Assert.IsTrue(routesDescription.First().Contains(inPrice.ToString()), "Rental description retrieved by GetUserRoutes doesn't contain the price");
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect GetUserRoutes service execution. Exception: " + e.Message);
            }
        }


        [TestMethod]
        public void NoLoggedUserGetRoutes()
        {
            InitDatabase();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //ecoScooter has 1 employee, 1 user, 1 station , 1 scooter adn 2 rentals
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.GetUserRoutes(START_INTERVAL, END_INTERVAL),
                "GetUserRoutes doesn't control if there is an user is logged");
        }
        [TestMethod]
        public void BadIntervalDatesUserGetRoutes()
        {
            InitDatabase();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);

            //ecoScooter has 1 employee, 1 user, 1 station , 1 scooter adn 2 rentals
            ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.GetUserRoutes(END_INTERVAL, START_INTERVAL),
                "GetUserRoutes doesn't control if the start of the interval od dates is lower than the end");
        }
        [TestMethod]
        public void NoRoutesUserGetRoutes()
        {
            InitDatabase();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);

            //ecoScooter has 1 employee, 1 user, 1 station , 1 scooter adn 2 rentals
            ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually
            DateTime endInterval = START_INTERVAL.AddMinutes(1);

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.GetUserRoutes(START_INTERVAL, endInterval),
                "GetUserRoutes doesn't throws an exception if there aren't rentals in the period");
        }
        [TestMethod]
        public void GetRouteIdsRightData()
        {
            int rentalInsideId = InitDatabase().Id; //It returns the rental inside the interval
            //Create the service
            //ecoScooter has 1 employee, 1 user, 1 station , 1 scooter and 2 rentals
            ecoScooterService = new EcoScooterService(dal);
            try
            {
                ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually
                ICollection<string> routesDescription = ecoScooterService.GetUserRoutesIds(START_INTERVAL, END_INTERVAL);

                Assert.IsTrue(routesDescription.Count == 1, "GetUserRoutesIds doesn't retrieve the rental in the interval");
                Assert.AreEqual(rentalInsideId.ToString(), routesDescription.First(), "GetUserRoutesIds doesn't retrieve the rentalId  of the rental in the interval");

            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect GetUserRoutes service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void NoLoggedUserGetRouteIds()
        {
            InitDatabase();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //ecoScooter has 1 employee, 1 user, 1 station , 1 scooter adn 2 rentals
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.GetUserRoutesIds(START_INTERVAL, END_INTERVAL),
                "GetUserRoutesIds doesn't control if there is an user is logged");
        }
        [TestMethod]
        public void BadIntervalDatesUserGetRouteIds()
        {
            InitDatabase();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);

            //ecoScooter has 1 employee, 1 user, 1 station , 1 scooter adn 2 rentals
            ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.GetUserRoutesIds(END_INTERVAL, START_INTERVAL),
                "GetUserRoutesIds doesn't control if the start of the interval od dates is lower than the end");
        }
        [TestMethod]
        public void NoRoutesUserGetRouteIds()
        {
            InitDatabase();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);

            //ecoScooter has 1 employee, 1 user, 1 station , 1 scooter adn 2 rentals
            ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually
            DateTime endInterval = START_INTERVAL.AddMinutes(1);

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.GetUserRoutesIds(START_INTERVAL, endInterval),
                "GetUserRoutesIds doesn't throws an exception if there aren't rentals in the period");
        }
        [TestMethod]
        public void GetRouteDescriptionRightData()
        {
            Rental rentalId = InitDatabase();

            ecoScooterService = new EcoScooterService(dal);

            //ecoScooter has 1 employee, 1 user, 1 station , 1 scooter adn 2 rentals
            try
            {
                ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD); //It is not possible to check individually
                ecoScooterService.GetRouteDescription(rentalId.Id, out DateTime startDate, out DateTime endDate, out decimal price, out String originStationId, out String destinationStationId);


                Assert.AreEqual(rentalId.StartDate, startDate, "GetRouteDescription doesn't retrieve of the rental in the interval");
                Assert.AreEqual(rentalId.EndDate, endDate, "GetRouteDescription doesn't retrieve of the rental in the interval");
                Assert.AreEqual(rentalId.Price, price, "GetRouteDescription doesn't retrieve of the rental in the interval");
                Assert.AreEqual(rentalId.OriginStation.Id, originStationId, "GetRouteDescription doesn't retrieve of the rental in the interval");
                Assert.AreEqual(rentalId.DestinationStation.Id, destinationStationId, "GetRouteDescription doesn't retrieve of the rental in the interval");
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect GetUserRoutes service execution. Exception: " + e.Message);
            }

        }
        [TestMethod]
        public void NoLoggedGetRouteDescription()
        {
            Rental rentalId = InitDatabase();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);

            //ecoScooter has 1 employee, 1 user, 1 station , 1 scooter adn 2 rentals
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.GetRouteDescription(rentalId.Id, out DateTime startDate, out DateTime endDate,
               out decimal price, out String originStationId, out String destinationStationId),
              "GetUserRoutesIds doesn't control if there is an user is logged");
        }
    }
}
