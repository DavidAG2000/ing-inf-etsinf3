using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EcoScooter.Entities;

namespace UnitTestEcoScooterClassLibrary
{
    [TestClass]
    public class EcoScooterTest
    {
        private const int NO_ITEMS = 0;
        private const double EXPECTED_DISCOUNT = 0.15;
        private const double EXPECTED_FARE = 0.12;
        private const double EXPECTED_SPEED = 50;
        private const int EXPECTED_NUM_EXPLOYEES = 1;

        [TestMethod]
        public void NoParamsConstructorInitializesPeople()
        {
           
                EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter();              
                Assert.IsNotNull(ecoScooter.People, "Collection of Person no properly initialized.Patch the problem adding:  People = new List<Person>();");
           

        }
        [TestMethod]
        public void NoParamsConstructorInitializesEmployees()
        {
            
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(); 
            Assert.IsNotNull(ecoScooter.Employees,"Collection of Employee no properly initialized.\n Patch the problem adding:  Employees = new List<Employee>();");
            
        }
        [TestMethod]
        public void NoParamsConstructorInitializesScooters()
        {
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter();
            Assert.IsNotNull(ecoScooter.Scooters,"Collection of Scooter no properly initialized.\n Patch the problem adding:  Scooters = new List<Scooter>();");

        }
        [TestMethod]
        public void NoParamsConstructorInitializesStations()
        {
           EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter();
           Assert.IsNotNull(ecoScooter.Stations,"Collection of Station no properly initialized.\n Patch the problem adding:  Stations = new List<Station>();");
        }
        [TestMethod]
        public void ConstructorInitializesPropsAndEmployees()
        {
            try
            {
                EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT,EXPECTED_FARE,EXPECTED_SPEED, new Employee());
                int numEmployees = ecoScooter.Employees.Count;
                Assert.AreEqual(EXPECTED_DISCOUNT, ecoScooter.DiscountYounger, "DiscountYounger no properly initialized. Check params order and assigment");
                Assert.AreEqual(EXPECTED_FARE, ecoScooter.Fare, "Fare no properly initialized.Check the params order");
                Assert.AreEqual(EXPECTED_SPEED, ecoScooter.MaxSpeed, "MaxSpeed no properly initialized.Check params order and assigment");
                Assert.AreEqual(EXPECTED_NUM_EXPLOYEES, numEmployees, "The employee is not added to the collection Employees in the constructor with params");
            }
            catch (System.NullReferenceException )
            {
                Assert.Fail("Collection of Employee no properly initialized.\n Patch the problem adding:  Employee = new List<Employee>();");
            }

        }
        [TestMethod]
        public void ConstructorInitializesPeople()
        {
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter();
            Assert.IsNotNull(ecoScooter.People, "Collection of Person no properly initialized.Patch the problem adding:  People = new List<Person>();");

        }
        [TestMethod]
        public void ConstructorInitializesScooters()
        {
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter();
            Assert.IsNotNull(ecoScooter.Scooters, "Collection of Scooter no properly initialized.\n Patch the problem adding:  Scooters = new List<Scooter>();");

        }
        [TestMethod]
        public void ConstructorInitializesStations()
        {
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter();
            Assert.IsNotNull(ecoScooter.Stations, "Collection of Station no properly initialized.\n Patch the problem adding:  Stations = new List<Station>();");
        }

    }
}               
        