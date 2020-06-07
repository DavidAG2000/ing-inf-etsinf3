using EcoScooter.Entities;
using EcoScooter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestServiceEcoScooter
{
    [TestClass]
    public class RegisterScooterTests : BaseTest
    {
        //Station data
        private const string EXPECTED_ADDRESS = "Camino de Vera s/n. 46022 Valencia";
        private const string EXPECTED_ID = "UPV";
        private const string EXPECTED_EMPTY_ID = "";
        private const double EXPECTED_LAT = 39.482369;
        private const double EXPECTED_LONG = -0.343578;
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
        //EcoScooterData
        private const double EXPECTED_DISCOUNT = 0.15;
        private const double EXPECTED_FARE = 0.12;
        private const double EXPECTED_SPEED = 50;

        //Scooter Data
        private static DateTime EXPECTED_REGISTER_DATE = DateTime.Now.AddDays(-1);



        [TestMethod]
        public void NewScooterRightDataInMantenance()
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
                ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually
                ecoScooterService.RegisterScooter(EXPECTED_REGISTER_DATE, ScooterState.inMaintenance, EXPECTED_EMPTY_ID);
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect RegisterScooter service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void NewScooterRightDataAvailable()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);
            ecoScooter.Stations.Add(new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG));
            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            try
            {
                ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually
                ecoScooterService.RegisterScooter(EXPECTED_REGISTER_DATE, ScooterState.available, EXPECTED_ID);
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect RegisterScooter service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void NewScooterRightDataInMantenancePersits()
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
                ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually
                ecoScooterService.RegisterScooter(EXPECTED_REGISTER_DATE, ScooterState.inMaintenance, EXPECTED_EMPTY_ID);

                EcoScooter.Entities.EcoScooter ecoScooterDAL = dal.GetById<EcoScooter.Entities.EcoScooter>(ecoScooter.Id);
                Assert.IsNotNull(ecoScooterDAL, "EcoScooter is not updated in the database by RegisterScooter service");
                Assert.IsTrue(ecoScooterDAL.Scooters.Count > 0, "Scooter is not added to the Scooters collection in the ecoScooter by RegisterScooter service");
                Assert.AreEqual(EXPECTED_REGISTER_DATE, ecoScooterDAL.Scooters.First().RegisterDate, "EcoScooter is not updated in the database by RegisterScooter service. The scooter is not in the Sccoters list");
                Assert.AreEqual(ScooterState.inMaintenance, ecoScooterDAL.Scooters.First().State, "EcoScooter is not updated in the database by RegisterScooter service. The scooter is not in the Sccoters list");

                Scooter scooterDAL = dal.GetAll<Scooter>().First();
                Assert.IsNotNull(scooterDAL, "Scooter is not stored in the database by RegisterScooter service");
                Assert.AreEqual(EXPECTED_REGISTER_DATE, scooterDAL.RegisterDate, "Scooter is not stored in the database by RegisterScooter service");
                Assert.AreEqual(scooterDAL.State, ScooterState.inMaintenance, "Scooter is not stored in the database by RegisterScooter service");

            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect RegisterScooter service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void NewScooterRightDataAvailablePersists()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);
            ecoScooter.Stations.Add(new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG));
            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            try
            {
                ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually
                ecoScooterService.RegisterScooter(EXPECTED_REGISTER_DATE, ScooterState.available, EXPECTED_ID);
                EcoScooter.Entities.EcoScooter ecoScooterDAL = dal.GetById<EcoScooter.Entities.EcoScooter>(ecoScooter.Id);
                Assert.IsNotNull(ecoScooterDAL, "EcoScooter is not updated in the database by RegisterScooter service");
                Assert.AreEqual(EXPECTED_REGISTER_DATE, ecoScooterDAL.Scooters.First().RegisterDate, "EcoScooter is not updated in the database by RegisterScooter service. The scooter is not in the Sccoters list");
                Assert.AreEqual(ScooterState.available, ecoScooterDAL.Scooters.First().State, "EcoScooter is not updated in the database by RegisterScooter service. The scooter is not in the Sccoters list");

                Scooter scooterDAL = dal.GetAll<Scooter>().First();
                Assert.IsNotNull(scooterDAL, "Scooter is not stored in the database by RegisterScooter service");
                Assert.IsTrue(ecoScooterDAL.Scooters.Count > 0, "Scooter is not added to the Scooters collection in the ecoScooter by RegisterScooter service");
                Assert.AreEqual(EXPECTED_REGISTER_DATE, scooterDAL.RegisterDate, "Scooter is not stored in the database by RegisterScooter service");
                Assert.AreEqual(ScooterState.available, scooterDAL.State, "Scooter is not stored in the database by RegisterScooter service");

            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect RegisterScooter service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void NotLoggedEmployee()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterScooter(EXPECTED_REGISTER_DATE, ScooterState.inMaintenance, EXPECTED_EMPTY_ID),
               "RegisterScooter doesn't control if there is an employee logged");

        }
        [TestMethod]
        public void BadRegisterDate()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually

            DateTime BAD_REGISTER_DATE = DateTime.Now.AddDays(1);

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterScooter(BAD_REGISTER_DATE, ScooterState.inMaintenance, EXPECTED_EMPTY_ID),
               "RegisterScooter doesn't control if the RegisterDate is in the future");
        }
        [TestMethod]
        public void BadStationId()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterScooter(EXPECTED_REGISTER_DATE, ScooterState.available, EXPECTED_EMPTY_ID),
               "RegisterScooter doesn't control if the station doesn't exist");
        }

    }
}
