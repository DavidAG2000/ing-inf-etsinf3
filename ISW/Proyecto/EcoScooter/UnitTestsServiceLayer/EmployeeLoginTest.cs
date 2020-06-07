using EcoScooter.Entities;
using EcoScooter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestServiceEcoScooter
{
    [TestClass]
    public class EmployeeLoginTest : BaseTest //Base tests initilizes dal and ecoservice Before each test. Cleans database after each tests
    {
        //EcoScooterData
        private const double EXPECTED_DISCOUNT = 0.15;
        private const double EXPECTED_FARE = 0.12;
        private const double EXPECTED_SPEED = 50;
        //EmployeeData
        private static int TWENTY_AGE = 20;
        private static DateTime EXPECTED_BIRTHDATE = Date​Time.Now.AddYears(-TWENTY_AGE);
        private const string EXPECTED_DNI = "14422178M";
        private const string EXPECTED_EMAIL = "somebody@outlook.es";
        private const string EXPECTED_NAME = "Somebody White";
        private const int EXPECTED_TELEPHON = 6006787;
        private const string EXPECTED_IBAN = "ES6621000418401234567891";
        private const int EXPECTED_PIN = 1234;
        private const string EXPECTED_POSITION = "manager";
        private const decimal EXPECTED_SALARY = 1250;

        private const int BAD_PIN = 000;
        private const string BAD_DNI = "11111111B";


        [TestMethod]
        public void EmployeeExistsGoodPin()
        {

            //Insert an employee in the system
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                 EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //Call the service
            try
            {
                this.ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN);

            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect LoginEmployee service execution: employee isn't logged.\n Exception message: " + e.Message);
            }
        }
        [TestMethod]
        public void EmployeeAlreadyLoggedIn()
        {

            //Insert an employee in the system
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                 EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //Call the service
            try
            {
                this.ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN);

            }
            catch (Exception)
            {
                Assert.Fail("Incorrect LoginEmployee service execution: employee isn't logged ");
            }

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN), "LoginEmployee doesn't throws exception when double loggin");

        }
        [TestMethod]
        public void EmployeeExistsBadPin()
        {
            //Insert an employee in the system
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                 EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //Call the service
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.LoginEmployee(EXPECTED_DNI, BAD_PIN), "LoginEmployee doesn't throws  exception when bad pin");
        }
        [TestMethod]
        public void EmployeeNoExists()
        {
            //Insert an employee in the system
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                 EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //Call the service
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.LoginEmployee(BAD_DNI, BAD_PIN), "LoginEmployee doesn't throws the exception when bad dni");
        }
        [TestMethod]
        public void EmptyEmployee()
        {
            //Insert an employee in the system

            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter();

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //Call the service
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.LoginEmployee(BAD_DNI, BAD_PIN), "LoginEmployee doesn't throws the exception when no employees in the system");
        }
    }
}
