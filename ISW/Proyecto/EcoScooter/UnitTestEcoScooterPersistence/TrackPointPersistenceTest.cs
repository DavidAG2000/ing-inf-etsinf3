using EcoScooter.Entities;
using EcoScooter.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestEcoScooterPersistence
{
    [TestClass]
    public class TrackPointPersistenceTest
    {
        //TrackPointData
        private const double EXPECTED_BATTERY_LEVEL = 88.5;
        private const double EXPECTED_LAT = 55;
        private const double EXPECTED_LONG = 180;
        private const double EXPECTED_SPEED = 20;
        private static DateTime EXPECTED_TIMESTAMP = new Date​Time(2019, 07, 27, 21, 30, 00);
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
            TrackPoint trackPoint = new TrackPoint(EXPECTED_BATTERY_LEVEL, EXPECTED_LAT, EXPECTED_LONG,
                EXPECTED_SPEED, EXPECTED_TIMESTAMP);
            dal.Insert(trackPoint);
            dal.Commit();

            TrackPoint trackPointDal = dal.GetAll<TrackPoint>().First();

            Assert.AreEqual(EXPECTED_BATTERY_LEVEL, trackPointDal.BatteryLevel, "BatteryLevel not properly store");
            Assert.AreEqual(EXPECTED_LAT, trackPointDal.Latitude, "Latitude not properly store");
            Assert.AreEqual(EXPECTED_LONG, trackPointDal.Longitude, "Longitude not properly store");
            Assert.AreEqual(EXPECTED_SPEED, trackPointDal.Speed, "Speed not properly store");
            Assert.AreEqual(EXPECTED_TIMESTAMP, trackPointDal.Timestamp, "Timestamp not properly store");

        }


    }
}
