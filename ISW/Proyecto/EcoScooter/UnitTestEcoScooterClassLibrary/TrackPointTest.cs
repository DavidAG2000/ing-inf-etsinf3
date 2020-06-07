using System;
using EcoScooter.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterClassLibrary
{
    [TestClass]
    public class TrackPointTest
    {
        //TrackPointData
        private const double EXPECTED_BATTERY_LEVEL = 88.5;
        private const double EXPECTED_LAT = 55;
        private const double EXPECTED_LONG = 180;
        private const double EXPECTED_SPEED = 20;
        private static DateTime EXPECTED_TIMESTAMP = new Date​Time(2019, 07, 27, 21, 30, 00);

        [TestMethod]
        public void NoParametersConstructor()
        {
            TrackPoint trackPoint = new TrackPoint();
            Assert.AreNotSame(null, trackPoint, "We need a constructor wihtout parameters");

        }
        [TestMethod]
        public void ParamsConstructorIntilizesProps()
        {
            TrackPoint trackPoint = new TrackPoint(EXPECTED_BATTERY_LEVEL, EXPECTED_LAT, EXPECTED_LONG,
                EXPECTED_SPEED, EXPECTED_TIMESTAMP);
            Assert.AreEqual(EXPECTED_BATTERY_LEVEL, trackPoint.BatteryLevel, "BatteryLevel not properly initialized.Check params order and assigment");
            Assert.AreEqual(EXPECTED_LAT, trackPoint.Latitude, "Latitude not properly initialized.Check params order and assigment");
            Assert.AreEqual(EXPECTED_LONG, trackPoint.Longitude, "Longitude not properly initialized.Check params order and assigment");
            Assert.AreEqual(EXPECTED_SPEED, trackPoint.Speed, "Speed not properly initialized.Check params order and assigment");
            Assert.AreEqual(EXPECTED_TIMESTAMP, trackPoint.Timestamp, "Timestamp not properly initialized.Check params order and assigment");

        }


    }
}
