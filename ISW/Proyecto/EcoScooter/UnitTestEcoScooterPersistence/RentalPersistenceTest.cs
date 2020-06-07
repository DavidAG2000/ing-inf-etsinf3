using EcoScooter.Entities;
using EcoScooter.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestEcoScooterPersistence
{

    [TestClass]
    public class RentalPersistenceTest
    { //Rental Data
        private static DateTime? EXPECTED_END_DATE = new Date​Time(2019, 10, 14, 16, 55, 56);
        private static decimal EXPECTED_PRICE = 23;
        private static DateTime EXPECTED_START_DATE = new Date​Time(2019, 10, 14, 16, 15, 0);
        // Station Data
        private const string EXPECTED_ADDRESS = "somewhere street";
        private const string EXPECTED_STATION_ID = "cityCenter";
        private const double EXPECTED_LAT = 10;
        private const double EXPECTED_LONG = 34;
        //User Data 
        private static DateTime EXPECTED_BIRTHDATE = new Date​Time(2000, 07, 27);
        private const string EXPECTED_DNI = "14422178M";
        private const string EXPECTED_EMAIL = "somebody@outlook.es";
        private const string EXPECTED_NAME = "Somebody White";
        private const int EXPECTED_TELEPHON = 6006787;
        private const int EXPECTED_CVV = 324;
        private static DateTime EXPECTED_EXPIRATION_DATE = new Date​Time(2022, 12, 31);
        private const string EXPECTED_LOGIN = "somebody";
        private const int EXPECTED_NUMBER = 1234567891;
        private const string EXPECTED_PASSWORD = "password";

        //Scooter Data
        private static DateTime EXPECTED_REGISTER_DATE = new Date​Time(2019, 10, 14);
        private const ScooterState EXPECTED_STATE = ScooterState.inMaintenance;

        //Incident Data
        private const string EXPECTED_DESCRIPTION = "Broken Light";
        private static DateTime EXPECTED_TIMESTAMP = new Date​Time(2019, 10, 14, 16, 40, 00);

        //TrackPointData
        private const double EXPECTED_BATTERY_LEVEL = 88.5;
        //same position as sttion
        private const double EXPECTED_SPEED = 20;
        //Use the same timestamp as Incident

        //Data Access Layer
        private EntityFrameworkDAL dal;

        /**
         * Initializes DAL and removes all data
        **/
        private void SettingUp()
        {
            try
            {
                dal = new EntityFrameworkDAL(new EcoScooterDbContext());
                dal.RemoveAllData();

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        [TestMethod]
        public void StoresInitialData()
        {
            SettingUp();
            Station expectedStation = new Station(EXPECTED_ADDRESS, EXPECTED_STATION_ID, EXPECTED_LAT, EXPECTED_LONG);
            User expectedUser = new User(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);
            Scooter expectedScooter = new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            Rental rental = new Rental(EXPECTED_END_DATE, EXPECTED_PRICE, EXPECTED_START_DATE, expectedStation, expectedScooter, expectedUser);
            dal.Insert(rental);
            dal.Commit();

            Rental rentalDal = dal.GetAll<Rental>().First();
            Station stationDal = dal.GetAll<Station>().First();
            Scooter scooterDal = dal.GetAll<Scooter>().First();
            User userDal = dal.GetAll<User>().First();

            Assert.AreEqual(EXPECTED_END_DATE, rentalDal.EndDate, "EndDate of Rental not properly stored");
            Assert.AreEqual(EXPECTED_PRICE, rentalDal.Price, "Priceof Rental not properly stored");
            Assert.AreEqual(EXPECTED_START_DATE, rentalDal.StartDate, "StartDate of Rental not properly stored");

            Assert.IsNotNull(rentalDal.TrackPoints, "Collection of TrackPoint no properly initialized.");

            Assert.IsNotNull(rentalDal.OriginStation, "OriginStation not properly initialized.");
            Assert.AreEqual(EXPECTED_STATION_ID, rentalDal.OriginStation.Id, "OriginStation of Rental not properly stored");
            Assert.AreEqual(EXPECTED_STATION_ID, stationDal.Id, "OriginStation of Rental not properly stored in Station table");

            Assert.IsNotNull(rentalDal.Scooter, "Scooter not properly initialized.");
            Assert.AreEqual(expectedScooter.Id, rentalDal.Scooter.Id, "Scooter  of Rental not properly stored");
            Assert.AreEqual(expectedScooter.Id, scooterDal.Id, "Scooter  of Rental not properly stored in Scooter Table");

            Assert.IsNotNull(rentalDal.User, "User not properly initialized.");
            Assert.AreEqual(EXPECTED_DNI, rentalDal.User.Dni, "User of Rental not properly stored");
            Assert.AreEqual(EXPECTED_DNI, userDal.Dni, "User of Rental not properly stored");


        }
        [TestMethod]
        public void StoresIncident()
        {
            SettingUp();
            Station expectedStation = new Station(EXPECTED_ADDRESS, EXPECTED_STATION_ID, EXPECTED_LAT, EXPECTED_LONG);
            User expectedUser = new User(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
              EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);
            Scooter expectedScooter = new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            Rental rental = new Rental(EXPECTED_END_DATE, EXPECTED_PRICE, EXPECTED_START_DATE, expectedStation, expectedScooter, expectedUser);
            Incident incident = new Incident(EXPECTED_DESCRIPTION, EXPECTED_TIMESTAMP);
            rental.Incident = incident;
            dal.Insert(rental);
            dal.Commit();

            Rental rentalDal = dal.GetAll<Rental>().First();
            Incident incidentDAL = dal.GetAll<Incident>().First();
            Assert.AreEqual(EXPECTED_DESCRIPTION, rentalDal.Incident.Description, "Incident from Rental not properly stored ");
            Assert.AreEqual(EXPECTED_TIMESTAMP, rentalDal.Incident.TimeStamp, "Incident from Rental not properly stored ");
            Assert.AreEqual(EXPECTED_DESCRIPTION, incidentDAL.Description, "Incident from Rental not properly stored in Incident table");
            Assert.AreEqual(EXPECTED_TIMESTAMP, incidentDAL.TimeStamp, "Incident from Rental not properly stored in Incident table");

        }
        [TestMethod]
        public void StoresTrackPoint()
        {
            SettingUp();
            Station expectedStation = new Station(EXPECTED_ADDRESS, EXPECTED_STATION_ID, EXPECTED_LAT, EXPECTED_LONG);
            User expectedUser = new User(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
             EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);
            Scooter expectedScooter = new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            Rental rental = new Rental(EXPECTED_END_DATE, EXPECTED_PRICE, EXPECTED_START_DATE, expectedStation, expectedScooter, expectedUser);
            TrackPoint trackPoint = new TrackPoint(EXPECTED_BATTERY_LEVEL, EXPECTED_LAT, EXPECTED_LONG,
                EXPECTED_SPEED, EXPECTED_TIMESTAMP);
            rental.TrackPoints.Add(trackPoint);
            dal.Insert(rental);
            dal.Commit();

            Rental rentalDal = dal.GetAll<Rental>().First();
            TrackPoint trackPointDal = dal.GetAll<TrackPoint>().First();

            Assert.IsNotNull(rentalDal.TrackPoints, "TrackPoints not properly initialized.");
            Assert.AreEqual(EXPECTED_BATTERY_LEVEL, rentalDal.TrackPoints.First().BatteryLevel, "TrackPoint from Rental not properly stored ");
            Assert.AreEqual(EXPECTED_BATTERY_LEVEL, trackPointDal.BatteryLevel, "TrackPoint from Rental not properly stored in TrackPoint database");

        }
        [TestMethod]
        public void StoresDestinationStation()
        {
            SettingUp();
            Station expectedStation = new Station(EXPECTED_ADDRESS, EXPECTED_STATION_ID, EXPECTED_LAT, EXPECTED_LONG);
            User expectedUser = new User(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                        EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);
            Scooter expectedScooter = new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            Rental rental = new Rental(EXPECTED_END_DATE, EXPECTED_PRICE, EXPECTED_START_DATE, expectedStation, expectedScooter, expectedUser);
            rental.DestinationStation = expectedStation;
            dal.Insert(rental);
            dal.Commit();

            Rental rentalDal = dal.GetAll<Rental>().First();
            Station stationDal = dal.GetAll<Station>().First();
            Assert.IsNotNull(rentalDal.DestinationStation, "DestinationStation not properly initialized.");
            Assert.AreEqual(EXPECTED_STATION_ID, rentalDal.OriginStation.Id, "OriginStation of Rental not properly stored");
            Assert.AreEqual(EXPECTED_STATION_ID, stationDal.Id, "OriginStation of Rental not properly stored in Station table");
        }
    }
}
