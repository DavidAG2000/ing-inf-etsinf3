using System;
using EcoScooter.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterClassLibrary
{ // Station Data
    
    [TestClass]
    public class StationTest
    {  //Station data
        private const string EXPECTED_ADDRESS = "somewhere street";
        private const string EXPECTED_ID = "cityCenter";
        private const double EXPECTED_LAT = 10;
        private const double EXPECTED_LONG = 34;

        [TestMethod]
        public void NoParamsConstructorInitializesCollections()
        {
            Station station = new Station();
            Assert.IsNotNull(station.DestinationRentals, "Collection of DestinationRental no properly initialized. \n Patch the problem adding: DestinationRentals = new List<Rental>();");
            Assert.IsNotNull(station.OriginRentals, "Collection of OriginRental no properly initialized. \n Patch the problem adding: OriginRentals = new List<Rental>();");
            Assert.IsNotNull(station.Scooters, "Collection of Scooter no properly initialized. \n Patch the problem adding: Scooters = new List<Scooter>();");
        }
        [TestMethod]
        public void ParamsConstructorIntilizesProps()
        {
            Station station = new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG);
            Assert.AreEqual(EXPECTED_ADDRESS,station.Address, "Address not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_ID, station.Id, "Id not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_LAT, station.Latitude, "Latitude not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_LONG, station.Longitude, "Longitude not properly initialized. Check params order and assigment");

            Assert.IsNotNull(station.DestinationRentals, "Collection of DestinationRental no properly initialized. \n Patch the problem adding: DestinationRentals = new List<Rental>();");
            Assert.IsNotNull(station.OriginRentals, "Collection of OriginRental no properly initialized. \n Patch the problem adding: OriginRentals = new List<Rental>();");
            Assert.IsNotNull(station.Scooters, "Collection of Scooter no properly initialized. \n Patch the problem adding: Scooters = new List<Scooter>();");

        }

    }
}
