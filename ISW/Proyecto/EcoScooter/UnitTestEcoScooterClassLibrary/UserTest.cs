using System;
using EcoScooter.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterClassLibrary
{
    [TestClass]
    public class UserTest
    {
        private static DateTime EXPECTED_BIRTHDATE = new Date​Time(2000, 07, 27);
        private const string EXPECTED_DNI = "14422178M";
        private const string EXPECTED_EMAIL = "somebody@outlook.es";
        private const string EXPECTED_NAME = "Somebody White";
        private const int EXPECTED_TELEPHON = 6006787;
        private const int EXPECTED_CVV = 324;
        private static DateTime EXPECTED_EXPIRATION_DATE = new Date​Time(2022, 12, 31);
        private const string EXPECTED_LOGIN = "somebody";
        private const int EXPECTED_NUMBER = 1234567891;
        private const string EXPECTED_PASSWORD = "password";




        [TestMethod]
        public void NoParamsConstructorInitializesMaintenances()
        {
            User user= new User();
            Assert.IsNotNull(user.Rentals, "Collection of Rental no properly initialized. \n Patch the problem adding:  Rentals = new List<Rental>();");
        }
        [TestMethod]
        public void ParamsConstructorIntilizesProps()
        {
            User user = new User(EXPECTED_BIRTHDATE,EXPECTED_DNI,EXPECTED_EMAIL,EXPECTED_NAME,EXPECTED_TELEPHON,
                EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER,EXPECTED_PASSWORD);
            Assert.AreEqual(EXPECTED_BIRTHDATE, user.Birthdate, "Birthdate not properly initialized. Check base call and params order");
            Assert.AreEqual(EXPECTED_DNI, user.Dni, "Dni  not properly initialized. Check base call and params order");
            Assert.AreEqual(EXPECTED_EMAIL, user.Email, "Email not properly initialized. Check base call and params order");
            Assert.AreEqual(EXPECTED_NAME, user.Name, "Name not properly initialized. Check base call and params order");
            Assert.AreEqual(EXPECTED_TELEPHON, user.Telephon, "Telephon not properly initialized. Check base call and params order");
            Assert.AreEqual(EXPECTED_CVV, user.Cvv, "Cvv not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_EXPIRATION_DATE, user.ExpirationDate, "ExperationDate not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_LOGIN, user.Login, "Login not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_NUMBER, user.Number, "Number not properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_PASSWORD, user.Password, "Password not properly initialized. Check params order and assigment");

            Assert.IsNotNull(user.Rentals, "Collection of Rental no properly initialized. \n Patch the problem adding:  Rentals = new List<Rental>();");

        }
    }
}
