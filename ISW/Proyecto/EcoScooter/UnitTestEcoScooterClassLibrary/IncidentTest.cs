using System;
using EcoScooter.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterClassLibrary
{
    [TestClass]
    public class IncidentTest
    {   //Incident Data
        private const string EXPECTED_DESCRIPTION = "Broken Light";
        private const int EXPECTED_ID = 4001;
        private static DateTime EXPECTED_TIMESTAMP = new Date​Time(2020, 07, 27, 21,30,00);


        [TestMethod]
        public void NoParametersConstructor()
        {
            Incident incident = new Incident();
            Assert.AreNotSame(null, incident, "We need a constructor wihtout parameters");

        }
        [TestMethod]
        public void ParamsConstructorIntilizesProps()
        {
            Incident incident = new Incident(EXPECTED_DESCRIPTION, EXPECTED_ID, EXPECTED_TIMESTAMP);
            Assert.AreEqual(EXPECTED_DESCRIPTION, incident.Description, "Description not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_ID, incident.Id, "Id not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_TIMESTAMP, incident.TimeStamp, "TimeStamp not properly initialized. Check params order and assigment");
        }

        
    }
}
