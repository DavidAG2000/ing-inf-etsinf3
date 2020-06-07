using EcoScooter.Entities;
using EcoScooter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestServiceEcoScooter
{
    [TestClass]
    public class RegisterUserTest : BaseTest
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

        //User Data
        private static readonly int LUSTRUM = 5;
        private static DateTime EXPECTED_USER_BIRTHDATE = Date​Time.Now.AddYears(-TWENTY_AGE + 1);
        private const string EXPECTED_USER_DNI = "29829859D";
        private const string EXPECTED_USER_EMAIL = "user@outlook.es";
        private const string EXPECTED_USER_NAME = "User Brown";
        private const int EXPECTED_USER_TELEPHON = 600331836;
        private const int EXPECTED_CVV = 123;
        private static DateTime EXPECTED_EXPIRATION_DATE = Date​Time.Now.AddYears(LUSTRUM);
        private const string EXPECTED_LOGIN = "user";
        private const int EXPECTED_NUMBER = 12345678;
        private const string EXPECTED_PASSWORD = "user_password";

        private const int EXPECTED_NUM_PEOPLE = 2;
        private static readonly int LESS_NUMBER_CARD = 1234567;
        private static readonly int MORE_NUMBER_CARD = 123456789;

        private static readonly int LESS_CVV = 12;
        private static readonly int MORE_CVV = 1234;
        private const int OVER_AGE = 16;

        [TestMethod]
        public void newUserRightData()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
               EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            try
            {//RegisterUser(DateTime birthDate, string dni, string email, string name, int telephon, int cvv, DateTime expirationDate, string login, int number, string password)
                ecoScooterService.RegisterUser(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME, EXPECTED_USER_TELEPHON,
                    EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect RegisterUSer service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void newUserRightDataPersists()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
               EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            try
            {
                ecoScooterService.RegisterUser(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME, EXPECTED_USER_TELEPHON,
                    EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);

                User userDAL = dal.GetById<User>(EXPECTED_USER_DNI);
                EcoScooter.Entities.EcoScooter ecoScooterDAL = dal.GetAll<EcoScooter.Entities.EcoScooter>().First();


                Assert.IsNotNull(userDAL, "User is not store in the database by RegisterUser service");
                Assert.IsNotNull(ecoScooterDAL, "EcoScooter is not update in the database by RegisterUser service");
                Assert.AreEqual(EXPECTED_USER_DNI, userDAL.Dni, "User is not store in the database by RegisterUser service");
                Assert.AreEqual(EXPECTED_NUM_PEOPLE, ecoScooterDAL.People.Count, "EcoScooter is not update in the database by RegisterUser service");
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect RegisterUSer service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void ExistingUser()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
              EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //dni already exists
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterUser(EXPECTED_USER_BIRTHDATE, EXPECTED_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME, EXPECTED_USER_TELEPHON,
                  EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD), "RegisterUser doesn't control if the user already exists");

        }
        [TestMethod]
        public void BadCreditCardNumberLessDigits()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
               EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //dni already exists
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterUser(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME, EXPECTED_USER_TELEPHON,
                  EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, LESS_NUMBER_CARD, EXPECTED_PASSWORD), "RegisterUser doesn't control if the credit card number has got less than 8 digits");

        }
        [TestMethod]
        public void BadCreditCardNumberMoreDigits()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
               EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //dni already exists
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterUser(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME, EXPECTED_USER_TELEPHON,
                  EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, MORE_NUMBER_CARD, EXPECTED_PASSWORD), "RegisterUser doesn't control if the credit card number has got less than 8 digits");

        }
        [TestMethod]
        public void BadCvvLessDigits()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
               EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);



            //dni already exists
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterUser(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME, EXPECTED_USER_TELEPHON,
                  LESS_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, MORE_NUMBER_CARD, EXPECTED_PASSWORD), "RegisterUser doesn't control if cvv has less than 3 digits");

        }
        [TestMethod]
        public void BadCvvMoreDigits()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
               EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);



            //dni already exists
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterUser(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME, EXPECTED_USER_TELEPHON,
                  MORE_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, MORE_NUMBER_CARD, EXPECTED_PASSWORD), "RegisterUser doesn't control if cvv has more than 3 digits");

        }
        [TestMethod]
        public void BadAge()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
               EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            DateTime youngBirthdate = DateTime.Now.AddYears(-OVER_AGE).AddDays(1);


            //dni already exists
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterUser(youngBirthdate, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME, EXPECTED_USER_TELEPHON,
                  EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD), "RegisterUser doesn't control if the user is under 16");

        }
        [TestMethod]
        public void ExistingLoginUser()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
              EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            ecoScooter.People.Add(new User(DateTime.Now.AddYears(TWENTY_AGE - 2), "12345680Q", EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD));
            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            //dni already exists
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterUser(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME, EXPECTED_USER_TELEPHON,
                  EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD), "RegisterUser doesn't control if the user login already exists");

        }
        [TestMethod]
        public void BadExpirationDate()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
              EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            DateTime badExpirationDate = DateTime.Now.AddDays(-1);
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterUser(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME, EXPECTED_USER_TELEPHON,
                    EXPECTED_CVV, badExpirationDate, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD), "RegisterUser doesn't control if expired data is in the past");
        }
    }
}
