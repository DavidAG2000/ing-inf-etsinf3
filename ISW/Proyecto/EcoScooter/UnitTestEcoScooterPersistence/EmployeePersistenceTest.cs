using EcoScooter.Entities;
using EcoScooter.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestEcoScooterPersistence
{
    [TestClass]
    public class EmployeePersistenceTest
    {   //EmployeeData
        private static DateTime EXPECTED_BIRTHDATE = new Date​Time(2000, 07, 27);
        private const string EXPECTED_DNI = "14422178M";
        private const string EXPECTED_EMAIL = "somebody@outlook.es";
        private const string EXPECTED_NAME = "Somebody White";
        private const int EXPECTED_TELEPHON = 6006787;
        private const string EXPECTED_IBAN = "ES6621000418401234567891";
        private const int EXPECTED_PIN = 1234;
        private const string EXPECTED_POSITION = "manager";
        private const decimal EXPECTED_SALARY = 1250;

        //MaintenanceData
        private const string EXPECTED_DESCRIPTION = "Revise batteries";
        private static DateTime? EXPECTED_END_DATE = new Date​Time(2020, 1, 14);
        private static DateTime EXPECTED_START_DATE = new Date​Time(2019, 10, 14);

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
            //DAL and clear data
            SettingUp();

            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            dal.Insert(employee);
            dal.Commit();

            //retrieve data from DAL
            Employee employeeDAL = dal.GetAll<Employee>().First();

            Assert.AreEqual(EXPECTED_BIRTHDATE, employeeDAL.Birthdate, " Birthdate of Employee  not properly stored");
            Assert.AreEqual(EXPECTED_DNI, employeeDAL.Dni, "Dni of Employee  not properly stored");
            Assert.AreEqual(EXPECTED_EMAIL, employeeDAL.Email, "Email of Employee  not properly stored");
            Assert.AreEqual(EXPECTED_NAME, employeeDAL.Name, "Name of Employee  not properly stored");
            Assert.AreEqual(EXPECTED_TELEPHON, employeeDAL.Telephon, "Telephon of Employee  not properly stored");
            Assert.AreEqual(EXPECTED_IBAN, employeeDAL.Iban, "Iban of Employee  not properly stored");
            Assert.AreEqual(EXPECTED_PIN, employeeDAL.Pin, "Pin of Employee  not properly stored");
            Assert.AreEqual(EXPECTED_POSITION, employeeDAL.Position, "Position of Employee  not properly stored");
            Assert.AreEqual(EXPECTED_SALARY, employeeDAL.Salary, "Salary of Employee  not properly stored");

        }
        [TestMethod]
        public void StoresMaintenance()
        {
            //DAL and clear data
            SettingUp();

            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);

            Maintenance maintenance = new Maintenance(EXPECTED_DESCRIPTION, EXPECTED_END_DATE, EXPECTED_START_DATE, employee);

            try
            {
                employee.Maintenances.Add(maintenance);
            }
            catch (System.NullReferenceException) //Adding is not possible 
            {
                Assert.Fail("Collection of Maintenance no properly initialized. \n");

            }

            dal.Insert(employee);
            dal.Commit();

            //retrieve data from DAL
            Employee employeeDAL = dal.GetAll<Employee>().First();
            Assert.AreEqual(EXPECTED_DNI, employeeDAL.Dni, "Dni of Employee not properly stored");
            Assert.AreEqual(EXPECTED_DESCRIPTION, employeeDAL.Maintenances.First().Description, "Maintenance of Employee not properly stored");
            Assert.AreEqual(EXPECTED_END_DATE, employeeDAL.Maintenances.First().EndDate, "Maintenance of Employee not properly stored");
            Assert.AreEqual(EXPECTED_START_DATE, employeeDAL.Maintenances.First().StartDate, "Maintenance of Employee not properly stored");
            Assert.AreEqual(EXPECTED_DNI, employeeDAL.Maintenances.First().Employee.Dni, "Maintenance of Employee not properly stored");


        }


    }
}
