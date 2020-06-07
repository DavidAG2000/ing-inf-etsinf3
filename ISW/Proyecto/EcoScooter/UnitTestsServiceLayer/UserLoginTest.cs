using EcoScooter.Entities;
using EcoScooter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestServiceEcoScooter
{
    [TestClass]
    public class UserLoginTest : BaseTest //Base tests initilizes dal and ecoservice Before each test. Cleans database after each tests
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

        //User data
        private static int LUSTRUM = 5;
        private static DateTime EXPECTED_USER_BIRTHDATE = Date​Time.Now.AddYears(-TWENTY_AGE + 1);
        private const string EXPECTED_USER_DNI = "12345678Z";
        private const string EXPECTED_USER_EMAIL = "user@outlook.es";
        private const string EXPECTED_USER_NAME = "User White";
        private const int EXPECTED_USER_TELEPHON = 60011111;
        private const int EXPECTED_CVV = 324;
        private static DateTime EXPECTED_EXPIRATION_DATE = Date​Time.Now.AddYears(LUSTRUM);
        private const string EXPECTED_LOGIN = "userLogin";
        private const int EXPECTED_NUMBER = 1234567891;
        private const string EXPECTED_PASSWORD = "password";
        private const string BAD_LOGIN = "badLogin";
        private const string BAD_PASSWORD = "badPassword";




        [TestMethod]
        public void GoodLoginData()
        {

            //Insert an employee an a user in the system
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                 EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);
            User user = new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
                EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);
            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //Call the service
            try
            {
                this.ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD);

            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect LoginUser service execution: user isn't logged. \nException message: " + e.Message);
            }
        }
        [TestMethod]
        public void UserAlreadyLoggedIn()
        {

            //Insert an employee in the system
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                 EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);
            User user = new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
               EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);
            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //Call the service
            try
            {
                this.ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD);

            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect LoginUser service execution: user isn't logged. Exception: " + e.Message);
            }

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD), "LoginUser doesn't throws exception when double loggin");

        }
        [TestMethod]
        public void UserExistsBadPassword()
        {
            //Insert an employee in the system
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                 EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);
            User user = new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
              EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);
            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //Call the service
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.LoginUser(EXPECTED_LOGIN, BAD_PASSWORD), "LoginUser doesn't throws  exception when bad password");
        }
        [TestMethod]
        public void UserNoExists()
        {
            //Insert an employee in the system
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                 EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);
            User user = new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME,
                        EXPECTED_USER_TELEPHON, EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            ecoScooter.People.Add(user);
            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //Call the service
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.LoginUser(BAD_LOGIN, EXPECTED_PASSWORD), "LoginUSer doesn't throws the exception when bad login");
        }
        [TestMethod]
        public void EmptyUser()
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
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.LoginUser(EXPECTED_LOGIN, EXPECTED_PASSWORD), "LoginUser doesn't throws the exception when no users in the system");
        }
    }
}
