using System;
using EcoScooter.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterClassLibrary
{

    [TestClass]
    public class MaintenanceTest
    {
        private const string EXPECTED_DESCRIPTION = "Revise batteries";
        private static DateTime? EXPECTED_END_DATE = new Date​Time(2020, 1, 14);
        private static DateTime? EXPECTED_NULL_DATE = null;
        private const int EXPECTED_ID = 1001;
        private static DateTime EXPECTED_START_DATE = new Date​Time(2019, 10, 14);
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

        [TestMethod]
        public void NoParamsConstructorInitializesPlanningWork()
        {
            Maintenance maintenance = new Maintenance();
            Assert.IsNotNull(maintenance.PlanningWork, "Collection of PlanningWork no properly initialized. \n Patch the problem adding: PlanningWork = new List<PlanningWork>();");


        }
        [TestMethod]
        public void ParamsConstructorIntilizesProps()
        {
            Maintenance maintenance = new Maintenance(EXPECTED_DESCRIPTION, EXPECTED_END_DATE, EXPECTED_ID, EXPECTED_START_DATE, EXPECTED_EMPLOYEE);
            Assert.AreEqual(EXPECTED_DESCRIPTION, maintenance.Description, "Description not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_END_DATE, maintenance.EndDate, "EndDate not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_ID, maintenance.Id, "Id not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_START_DATE, maintenance.StartDate, "StartDate not properly initialized. Check params order and assigment");

            Assert.IsNotNull(maintenance.PlanningWork, "Collection of PlanningWork no properly initialized. \n Patch the problem adding: PlanningWork = new List<PlanningWork>();");
            Assert.IsNotNull(maintenance.Employee, "Employee is not properly initialized. Check assigment");
            Assert.AreEqual(EXPECTED_DNI, maintenance.Employee.Dni, "Employee not properly initialized. Check assigment");

        }
        [TestMethod]
        public void ParamsConstructorCanIntilizeEndDateToNull()
        {
            Maintenance maintenance = new Maintenance(EXPECTED_DESCRIPTION, null, EXPECTED_ID, EXPECTED_START_DATE, EXPECTED_EMPLOYEE);
            Assert.AreEqual(EXPECTED_NULL_DATE, maintenance.EndDate, "EndDate should be able to receive a null value");

        }

    }
}