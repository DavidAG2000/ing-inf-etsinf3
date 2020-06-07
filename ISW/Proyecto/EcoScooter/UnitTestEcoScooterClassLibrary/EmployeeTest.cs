using System;
using EcoScooter.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterClassLibrary
{
    [TestClass]
    public class EmployeeTest
    {
        private static DateTime EXPECTED_BIRTHDATE = new Date​Time(2000, 07, 27);
        private const string EXPECTED_DNI = "14422178M";
        private const string EXPECTED_EMAIL = "somebody@outlook.es";
        private const string EXPECTED_NAME = "Somebody White";
        private const int EXPECTED_TELEPHON = 6006787;
        private const string EXPECTED_IBAN = "ES6621000418401234567891";
        private const int EXPECTED_PIN = 1234;
        private const string EXPECTED_POSITION = "manager";
        private const decimal EXPECTED_SALARY = 1250;

        [TestMethod]
        public void NoParamsConstructorInitializesMaintenances()
        {
            Employee employee= new Employee();
            Assert.IsNotNull(employee.Maintenances, "Collection of Maintenance no properly initialized. \n Patch the problem adding:  Maintenance = new List<Maintenance>();");
        }
        [TestMethod]
        public void ParamsConstructorIntilizesProps()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE,EXPECTED_DNI,EXPECTED_EMAIL,EXPECTED_NAME,EXPECTED_TELEPHON,
                EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            Assert.AreEqual(EXPECTED_BIRTHDATE, employee.Birthdate, "Birthdate not properly initialized. Check base call and params order");
            Assert.AreEqual(EXPECTED_DNI, employee.Dni, "Dni  not properly initialized. Check base call and params order");
            Assert.AreEqual(EXPECTED_EMAIL, employee.Email, "Email not properly initialized. Check base call and params order");
            Assert.AreEqual(EXPECTED_NAME, employee.Name, "Name not properly initialized. Check base call and params order");
            Assert.AreEqual(EXPECTED_TELEPHON, employee.Telephon, "Telephon not properly initialized. Check base call and params order");
            Assert.AreEqual(EXPECTED_IBAN, employee.Iban, "Iban not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_PIN, employee.Pin, "Pin not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_POSITION,employee.Position, "Position not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_SALARY,employee.Salary, "Salary not properly initialized. Check params order and assigment");
            Assert.IsNotNull(employee.Maintenances, "Collection of Maintenance no properly initialized");
        }
    }
}
