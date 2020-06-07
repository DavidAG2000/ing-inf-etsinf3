using EcoScooter.Entities;
using EcoScooter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestServiceEcoScooter
{
    [TestClass]
    public class RegisterStationTest : BaseTest
    {  //Station data
        private const string EXPECTED_ADDRESS = "Camino de Vera s/n. 46022 Valencia";
        private const string EXPECTED_ID = "UPV";
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

        private const double MAX_LATITUDE = 90;
        private const double MAX_LONGITUDE = 180;

        [TestMethod]
        public void NewStationRightData()
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
                ecoScooterService.RegisterStation(EXPECTED_ADDRESS, EXPECTED_LAT, EXPECTED_LONG, EXPECTED_ID);
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect RegisterStation service execution. Exception: " + e.Message);
            }
        }
        [TestMethod]
        public void NewStationRightDataPersists()
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
                ecoScooterService.RegisterStation(EXPECTED_ADDRESS, EXPECTED_LAT, EXPECTED_LONG, EXPECTED_ID);

                EcoScooter.Entities.EcoScooter ecoScooterDAL = dal.GetById<EcoScooter.Entities.EcoScooter>(ecoScooter.Id);
                Station stationDAL = dal.GetById<Station>(EXPECTED_ID);
                Assert.IsNotNull(stationDAL, "Station is not stored in the database by RegisterStation service");
                Assert.IsNotNull(ecoScooterDAL, "EcoScooter is not updated in the database by RegisterStation service");
                Assert.AreEqual(EXPECTED_ID, ecoScooterDAL.Stations.First().Id, "EcoScooter is not updated in the database by RegisterStation service");
            }
            catch (Exception e)
            {
                Assert.Fail("Incorrect RegisterStation service execution. Exception: " + e.Message);
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

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterStation(EXPECTED_ADDRESS, EXPECTED_LAT, EXPECTED_LONG, EXPECTED_ID),
                "RegisterStation doesn't control if there is an employee is logged");
        }
        [TestMethod]
        public void StationIdAlreadyExists()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);
            ecoScooter.Stations.Add(new Station(EXPECTED_ADDRESS, EXPECTED_ID, EXPECTED_LAT, EXPECTED_LONG));
            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterStation(EXPECTED_ADDRESS, EXPECTED_LAT, EXPECTED_LONG, EXPECTED_ID),
                         "RegisterStation doesn't control if the station id already exists");
        }
        [TestMethod]
        public void BadAddress()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually
            string BAD_ADDRESS = "";
            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterStation(BAD_ADDRESS, EXPECTED_LAT, EXPECTED_LONG, EXPECTED_ID),
                         "RegisterStation doesn't control if the address is icorrect");
        }
        [TestMethod]

        public void BadLatitudeLower()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually

            double BAD_LAT_UNDER = -MAX_LATITUDE - 1;

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterStation(EXPECTED_ADDRESS, BAD_LAT_UNDER, EXPECTED_LONG, EXPECTED_ID),
                         "RegisterStation doesn't control if the latitud is in the interval [-" + MAX_LATITUDE + "," + MAX_LATITUDE + "]");
        }
        [TestMethod]
        public void BadLatitudeOver()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually

            double BAD_LONG_OVER = MAX_LONGITUDE + 1;

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterStation(EXPECTED_ADDRESS, EXPECTED_LAT, BAD_LONG_OVER, EXPECTED_ID),
                         "RegisterStation doesn't control if the latitud is in the interval [-" + MAX_LONGITUDE + "," + MAX_LONGITUDE + "]");
        }
        [TestMethod]
        public void BadLongitudeOver()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually


            double BAD_LONG_OVER = MAX_LONGITUDE + 1;

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterStation(EXPECTED_ADDRESS, EXPECTED_LAT, BAD_LONG_OVER, EXPECTED_ID),
                         "RegisterStation doesn't control if the longitud is in the interval [-" + MAX_LONGITUDE + "," + MAX_LONGITUDE + "]");

        }
        [TestMethod]
        public void BadLongitudLower()
        {
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                    EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            //Create the service
            ecoScooterService = new EcoScooterService(dal);
            ecoScooterService.LoginEmployee(EXPECTED_DNI, EXPECTED_PIN); //It is not possible to check individually

            double BAD_LONG_UNDER = -MAX_LONGITUDE - 1;

            Assert.ThrowsException<ServiceException>(() => this.ecoScooterService.RegisterStation(EXPECTED_ADDRESS, EXPECTED_LAT, BAD_LONG_UNDER, EXPECTED_ID),
                         "RegisterStation doesn't control if the longitud is in the interval [-" + MAX_LONGITUDE + "," + MAX_LONGITUDE + "]");
        }
    }

}

