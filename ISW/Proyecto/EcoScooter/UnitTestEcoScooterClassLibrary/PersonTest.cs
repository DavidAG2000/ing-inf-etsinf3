using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EcoScooter.Entities;

namespace UnitTestEcoScooterClassLibrary
{
    [TestClass]
    public class PersonTest
    {   private static DateTime EXPECTED_BIRTHDATE = new Date​Time(2000, 07, 27);
        private const string EXPECTED_DNI = "14422178M";
        private const string EXPECTED_EMAIL = "somebody@outlook.es";
        private const string EXPECTED_NAME = "Somebody White";
        private const int EXPECTED_TELEPHON = 6006787;

        [TestMethod]
        public void NoParametersConstructor()
        {
            Person person = new Person();
            Assert.AreNotSame(null, person,"We need a constructor wihtout parameters");
            
        }
        [TestMethod]
        public void ConstructorInitializesProps()
        {
            Person person = new Person(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON);
            Assert.AreEqual(EXPECTED_BIRTHDATE, person.Birthdate, "Birthdate no properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_DNI, person.Dni, "Dni no properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_EMAIL, person.Email, "Email no properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_NAME, person.Name, "Name no properly initialized. Check params order and assigment");
            Assert.AreEqual(EXPECTED_TELEPHON, person.Telephon, "Teleohon no properly initialized. Check params order and assigment");


        }
    }
}
