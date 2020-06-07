using EcoScooter.Entities;
using EcoScooter.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestEcoScooterPersistence
{ // Station Data

    [TestClass]
    public class StationPersistenceTest
    {  //Station data
        private const string EXPECTED_ADDRESS = "somewhere street";
        private const string EXPECTED_ID = "cityCenter";
        private const double EXPECTED_LAT = 10;
        private const double EXPECTED_LONG = 34;
        //Rental Data
        private static DateTime? EXPECTED_END_DATE = new Date​Time(2019, 10, 14, 16, 55, 56);
        private static decimal EXPECTED_PRICE = 23;
        private static DateTime EXPECTED_START_DATE = new Date​Time(2019, 10, 14, 16, 15, 0);
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
            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            dal.Insert(station);
            dal.Commit();

            Station stationDal = dal.GetAll<Station>().First();

            Assert.AreEqual(EXPECTED_ADDRESS, stationDal.Address, "Address not properly store");
            Assert.AreEqual(EXPECTED_ID, stationDal.Id, "Id not properly store");
            Assert.AreEqual(EXPECTED_LAT, stationDal.Latitude, "Latitude not properly store");
            Assert.AreEqual(EXPECTED_LONG, stationDal.Longitude, "Longitude not properly store");

            Assert.IsNotNull(stationDal.DestinationRentals, "Collection of DestinationRental store");
            Assert.IsNotNull(stationDal.OriginRentals, "Collection of OriginRental store");
            Assert.IsNotNull(stationDal.Scooters, "Collection of Scooter no properly store");

        }
        [TestMethod]
        public void StoresDestinationRentals()
        {
            SettingUp();
            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            station.DestinationRentals.Add(new Rental(EXPECTED_END_DATE, EXPECTED_PRICE, EXPECTED_START_DATE, station,
                new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE),
                new User(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD)));
            dal.Insert(station);
            dal.Commit();

            Station stationDal = dal.GetAll<Station>().First();
            Rental rentalDal = dal.GetAll<Rental>().First();
            Assert.AreEqual(station.DestinationRentals.First().Id, stationDal.DestinationRentals.First().Id, "Destination rental of Station no properly store.");
            Assert.AreEqual(station.DestinationRentals.First().Id, rentalDal.Id, "Destination rental of Station no properly store in Rental database.");
            Assert.AreEqual(station.Id, rentalDal.DestinationStation.Id, "Destination rental of Station no properly store in Rental database.");
        }
        [TestMethod]
        public void StoresOriginRentals()
        {
            SettingUp();
            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            station.OriginRentals.Add(new Rental(EXPECTED_END_DATE, EXPECTED_PRICE, EXPECTED_START_DATE, station, new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE), new User(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD)));
            dal.Insert(station);
            dal.Commit();

            Station stationDal = dal.GetAll<Station>().First();
            Rental rentalDal = dal.GetAll<Rental>().First();
            Assert.AreEqual(station.OriginRentals.First().Id, stationDal.OriginRentals.First().Id, "Origin rental of Station no properly store.");
            Assert.AreEqual(station.OriginRentals.First().Id, rentalDal.Id, "Origin rental of Station no properly store in Rental database.");
            Assert.AreEqual(station.Id, rentalDal.OriginStation.Id, "Origin rental of Station no properly store in Rental database.");
        }
        [TestMethod]
        public void StoresScooter()
        {
            SettingUp();
            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            Scooter sccoter = new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            sccoter.Station = station;
            station.Scooters.Add(sccoter);


            dal.Insert(station);
            dal.Commit();
            Station stationDal = dal.GetAll<Station>().First();
            Scooter scooterDal = dal.GetAll<Scooter>().First();
            Assert.AreEqual(station.Scooters.First().Id, stationDal.Scooters.First().Id, "Scooter of Station no properly stored");
            Assert.AreEqual(station.Scooters.First().Id, scooterDal.Id, "Scooter of Station no properly stored in Scooter Database");
            Assert.AreEqual(station.Id, scooterDal.Station.Id, "Scooter of Station no properly stored in Scooter Database");


        }
    }
}
