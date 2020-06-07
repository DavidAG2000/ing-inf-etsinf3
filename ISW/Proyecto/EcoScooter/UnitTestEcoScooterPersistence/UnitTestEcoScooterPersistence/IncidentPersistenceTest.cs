using System;
using System.Linq;
using EcoScooter.Entities;
using EcoScooter.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterPersistence
{
    [TestClass]
    public class IncidentPersistenceTest
    {   //Incident Data
        private const string EXPECTED_DESCRIPTION = "Broken Light";
        private static DateTime EXPECTED_TIMESTAMP = new Date​Time(2020, 07, 27, 21, 30, 00);

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
        {   //DAL and clear data
            SettingUp();

            Incident incident = new Incident(EXPECTED_DESCRIPTION, EXPECTED_TIMESTAMP);
            dal.Insert(incident);
            dal.Commit();

            Incident incidentDAL = dal.GetAll<Incident>().First();
            
            Assert.AreEqual(EXPECTED_DESCRIPTION, incidentDAL.Description, "Description not properly stored");
            Assert.AreEqual(EXPECTED_TIMESTAMP, incidentDAL.TimeStamp, "TimeStamp not properly stored");
        }
    }
}
