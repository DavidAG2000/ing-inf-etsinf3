using System;
using EcoScooter.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterClassLibrary
{
    [TestClass]
    public class PlanningWorkTes
    {
        private const string EXPECTED_PLANNIG_DESCRIPTION = "Revise Battery";
        private const int EXPECTED_WORKING_TIME = 15;

     
        //EMPLOYEE DATA
        private static DateTime EXPECTED_BIRTHDATE = new Date​Time(2000, 07, 27);
        private const string EXPECTED_DNI = "14422178M";
        private const string EXPECTED_EMAIL = "somebody@outlook.es";
        private const string EXPECTED_NAME = "Somebody White";
        private const int EXPECTED_TELEPHON = 6006787;
        private const string EXPECTED_IBAN = "ES6621000418401234567891";
        private const int EXPECTED_PIN = 1234;
        private const string EXPECTED_POSITION = "manager";
        private const decimal EXPECTED_SALARY = 1250;
        private static Employee EXPECTED_EMPLOYEE = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
        //Maintenance DAta
        private const string EXPECTED_DESCRIPTION = "Revise batteries";
        private static DateTime? EXPECTED_END_DATE = new Date​Time(2020, 1, 14);
        private static DateTime? EXPECTED_NULL_DATE = null;
        private static int EXPECTED_ID = 1001;
        private static DateTime EXPECTED_START_DATE = new Date​Time(2019, 10, 14);
        private static Maintenance EXPECTED_MAINTENANCE = new Maintenance(EXPECTED_DESCRIPTION, EXPECTED_END_DATE, EXPECTED_ID, EXPECTED_START_DATE, EXPECTED_EMPLOYEE);

        //Scooter DAta
        //uses same id as maintenance 
        //uses same registerData as StardDAte of Maintenance
        private static ScooterState EXPECTED_STATE = ScooterState.inMaintenance;
        private static Scooter EXPECTED_SCOOTER = new Scooter(EXPECTED_ID, EXPECTED_START_DATE, EXPECTED_STATE);

        [TestMethod]
        public void NoParametersConstructor()
        {
            PlanningWork planningWork = new PlanningWork();
            Assert.AreNotSame(null, planningWork, "We need a constructor wihtout parameters");

        }
        [TestMethod]
        public void ParamsConstructorIntilizesProps()
        {
            PlanningWork planningWork = new PlanningWork(EXPECTED_PLANNIG_DESCRIPTION, EXPECTED_WORKING_TIME, EXPECTED_MAINTENANCE, EXPECTED_SCOOTER);

            Assert.AreEqual(EXPECTED_PLANNIG_DESCRIPTION, planningWork.Description, "Description no properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_WORKING_TIME, planningWork.WorkingTime, "WorkingTime no properly initialized. Check params order and assigment");
            Assert.IsNotNull(planningWork.Maintenance, "Maintenance is not properly initialized. Check assigment");
            Assert.IsNotNull(planningWork.Scooter, "Scooter  is not properly initialized. Check assigment");

        }
    }
}
