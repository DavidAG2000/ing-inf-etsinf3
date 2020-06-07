using EcoScooter.Entities;
using EcoScooter.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTestEcoScooterPersistence
{
    [TestClass]
    public class ScooterPersistenceTest
    {
        //Scooter Data
        private static DateTime EXPECTED_REGISTER_DATE = new Date​Time(2019, 10, 14);
        private const ScooterState EXPECTED_STATE = ScooterState.inMaintenance;

        //Rental Data
        private static DateTime? EXPECTED_END_DATE = new Date​Time(2019, 10, 14, 16, 55, 56);
        private static decimal EXPECTED_PRICE = 23;
        private static DateTime EXPECTED_START_DATE = new Date​Time(2019, 10, 14, 16, 15, 0);

        //User Data 
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

        // Station Data
        private const string EXPECTED_ADDRESS = "somewhere street";
        private const string EXPECTED_STATION_ID = "cityCenter";
        private const double EXPECTED_LAT = 10;
        private const double EXPECTED_LONG = 34;

        //MAINTENANCE DATA
        private const string EXPECTED_DESCRIPTION = "Revise batteries";

        //EMPLOYEE DATA       
        private const string EXPECTED_IBAN = "ES6621000418401234567891";
        private const int EXPECTED_PIN = 1234;
        private const string EXPECTED_POSITION = "manager";
        private const decimal EXPECTED_SALARY = 1250;

        //PLANINNG WORK DATA
        private const int EXPECTED_WORKING_TIME = 15;

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
            SettingUp();
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            dal.Insert(scooter);
            dal.Commit();
            Scooter scooterDal = dal.GetAll<Scooter>().First();
            Assert.AreEqual(EXPECTED_REGISTER_DATE, scooterDal.RegisterDate, "RegisterDate not properly stored. ");
            Assert.AreEqual(EXPECTED_STATE, scooterDal.State, "State not properly stored.");
            Assert.IsNotNull(scooterDal.PlanningWork, "Collection of PlanningWork no properly initialized .");
            Assert.IsNotNull(scooterDal.Rentals, "Collection of Rental no properly initialized.");


        }
        [TestMethod]
        public void StoresRental()
        {
            SettingUp();
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            User expectedUser = new User(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);
            scooter.Rentals.Add(new Rental(EXPECTED_END_DATE, EXPECTED_PRICE, EXPECTED_START_DATE,
                    new Station(EXPECTED_ADDRESS, EXPECTED_STATION_ID, EXPECTED_LAT, EXPECTED_LONG), scooter, expectedUser));
            dal.Insert(scooter);
            dal.Commit();

            Scooter scooterDal = dal.GetAll<Scooter>().First();
            Rental rentalDal = dal.GetAll<Rental>().First();
            Assert.IsNotNull(scooterDal.Rentals, "Collection of Rental no properly initialized.");
            Assert.AreEqual(scooter.Rentals.First().Id, scooterDal.Rentals.First().Id, "Rental of Scooter no properly stored");
            Assert.AreEqual(scooter.Rentals.First().Id, rentalDal.Id, "Rental of Scooter no properly stored in Rental database");

        }
        [TestMethod]
        public void StoresMaintenance()
        {
            SettingUp();
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            Employee expectedEmployee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            scooter.Maintenance = new Maintenance(EXPECTED_DESCRIPTION, EXPECTED_END_DATE, EXPECTED_START_DATE, expectedEmployee);
            dal.Insert(scooter);
            dal.Commit();

            Scooter scooterDal = dal.GetAll<Scooter>().First();
            Maintenance maintenanceDAL = dal.GetAll<Maintenance>().First();
            Assert.AreEqual(scooter.Maintenance.Id, scooterDal.Maintenance.Id, "Mainteance of Scooter no properly stored");
            Assert.AreEqual(scooter.Maintenance.Id, maintenanceDAL.Id, "Mainteance of Scooter no properly stored in Maintenence database");

        }
        [TestMethod]
        public void StoresStation()
        {
            SettingUp();
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            scooter.Station = new Station(EXPECTED_ADDRESS, EXPECTED_STATION_ID, EXPECTED_LAT, EXPECTED_LONG);
            scooter.Station.Scooters.Add(scooter);
            dal.Insert(scooter);
            dal.Commit();

            Scooter scooterDal = dal.GetAll<Scooter>().First();
            Station stationDal = dal.GetAll<Station>().First();

            Assert.AreEqual(scooter.Station.Id, scooterDal.Station.Id, "Station of Scooter no properly stored");
            Assert.AreEqual(scooter.Station.Id, stationDal.Id, "Station of Scooter no properly stored in Statio database");
        }

        [TestMethod]
        public void StoresPlanningWork()
        {
            SettingUp();
            Scooter scooter = new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            Employee expectedEmployee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
              EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            scooter.PlanningWork.Add(new PlanningWork(EXPECTED_DESCRIPTION, EXPECTED_WORKING_TIME,
                new Maintenance(EXPECTED_DESCRIPTION, EXPECTED_END_DATE, EXPECTED_START_DATE, expectedEmployee), scooter));
            dal.Insert(scooter);
            dal.Commit();

            Scooter scooterDal = dal.GetAll<Scooter>().First();
            PlanningWork planningWorkDAL = dal.GetAll<PlanningWork>().First();
            Assert.AreEqual(scooter.PlanningWork.First().Id, scooterDal.PlanningWork.First().Id, "PlanningWork of Scooter no properly stored");
            Assert.AreEqual(scooter.PlanningWork.First().Id, planningWorkDAL.Id, "PlanningWork of Scooter no properly stored in Planning Work Database");
        }
    }
}
