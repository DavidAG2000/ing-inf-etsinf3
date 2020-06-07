using System;
using EcoScooter.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterClassLibrary
{
    [TestClass]
    public class ScooterTest
    {
        private const int EXPECTED_ID = 1001;
        private static DateTime EXPECTED_REGISTER_DATE = new Date​Time(2019, 10, 14);
        private const ScooterState EXPECTED_STATE = ScooterState.inMaintenance;

        [TestMethod]
        public void NoParamsConstructorInitializesCollections()
        {
            Scooter scooter = new Scooter();
            Assert.IsNotNull(scooter.PlanningWork, "Collection of PlanningWork no properly initialized. \n Patch the problem adding: PlanningWork = new List<PlanningWork>();");
            Assert.IsNotNull(scooter.Rentals, "Collection of Rental no properly initialized. \n Patch the problem adding: Rentals = new List<Rental>();");
        }
        [TestMethod]
        public void ParamsConstructorIntilizesProps()
        {
            Scooter scooter = new Scooter(EXPECTED_ID, EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            Assert.AreEqual(EXPECTED_ID, scooter.Id, "Id not properly initialized.Check params order and assigment");
            Assert.AreEqual(EXPECTED_REGISTER_DATE, scooter.RegisterDate, "RegisterDate not properly initialized. Check assigment");
            Assert.AreEqual(EXPECTED_STATE, scooter.State, "State not properly initialized. Check assigment");

            Assert.IsNotNull(scooter.PlanningWork, "Collection of PlanningWork no properly initialized. \n Patch the problem adding: PlanningWork = new List<PlanningWork>();");
            Assert.IsNotNull(scooter.Rentals, "Collection of Rental no properly initialized. \n Patch the problem adding: Rentals = new List<Rental>();");
            

        }
    }
}
