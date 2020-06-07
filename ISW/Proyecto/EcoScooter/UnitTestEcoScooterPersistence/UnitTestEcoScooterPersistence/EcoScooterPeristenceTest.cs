using System;
using EcoScooter.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EcoScooter.Entities;
using System.Linq;

namespace UnitTestEcoScooterPersistence
{
    [TestClass]
    public class EcoScooterPeristenceTest
    {   //EcoScooterData
        private const double EXPECTED_DISCOUNT = 0.15;
        private const double EXPECTED_FARE = 0.12;
        private const double EXPECTED_SPEED = 50;
        private const int EXPECTED_NUM_EXPLOYEES = 1;
        private const int EXPECTED_NUM_USERS = 1;
        private const int EXPECTED_NUM_SCOOTERS = 1;
        //EmployeeData
        private static DateTime EXPECTED_BIRTHDATE = new Date​Time(2000, 07, 27);
        private const string EXPECTED_DNI = "14422178M";
        private const string EXPECTED_EMAIL = "somebody@outlook.es";
        private const string EXPECTED_NAME = "Somebody White";
        private const int EXPECTED_TELEPHON = 6006787;
        private const string EXPECTED_IBAN = "ES6621000418401234567891";
        private const int EXPECTED_PIN = 1234;
        private const string EXPECTED_POSITION = "manager";
        private const decimal EXPECTED_SALARY = 1250;

        //User Data
        private static DateTime EXPECTED_USER_BIRTHDATE = new Date​Time(2000, 05, 15);
        private const string EXPECTED_USER_DNI = "29829859D";
        private const string EXPECTED_USER_EMAIL = "user@outlook.es";
        private const string EXPECTED_USER_NAME = "User Brown";
        private const int EXPECTED_USER_TELEPHON = 600331836;
        private const int EXPECTED_CVV = 123;
        private static DateTime EXPECTED_EXPIRATION_DATE = new Date​Time(2022, 10, 31);
        private const string EXPECTED_LOGIN = "user";
        private const int EXPECTED_NUMBER = 1987654321;
        private const string EXPECTED_PASSWORD = "user_password";

        //Scooter Data
        private static DateTime EXPECTED_REGISTER_DATE = new Date​Time(2019, 10, 14);
        private const ScooterState EXPECTED_STATE = ScooterState.inMaintenance;

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
            //DAL and clear data
            SettingUp();
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT,EXPECTED_FARE,EXPECTED_SPEED,employee);

            dal.Insert(ecoScooter);
            dal.Commit();
            EcoScooter.Entities.EcoScooter ecoScooterDAL = dal.GetAll<EcoScooter.Entities.EcoScooter>().First();
            Employee employeeDAL = dal.GetAll<Employee>().First();
            try
            {
         
                Assert.AreEqual(EXPECTED_DISCOUNT, ecoScooterDAL.DiscountYounger, "DiscountYounger no properly stored");
                Assert.AreEqual(EXPECTED_FARE, ecoScooterDAL.Fare, "Fare no properly stored");
                Assert.AreEqual(EXPECTED_SPEED, ecoScooterDAL.MaxSpeed, "MaxSpeed no properly stored");
                Assert.AreEqual(EXPECTED_NUM_EXPLOYEES, ecoScooter.Employees.Count, "The employee is not stored");
                Assert.AreEqual(EXPECTED_DNI, ecoScooter.Employees.First().Dni, "Employee  not properly linked in EcoScooter");
                Assert.AreEqual(EXPECTED_DNI, employeeDAL.Dni, "Dni of Employee  not properly stored");
            }
            catch (System.NullReferenceException)
            {
                Assert.Fail("Collection of Employee no properly initialized.\n");
            }

            
        }
        [TestMethod]
        public void StoresPeople()
        {//DAL and clear data
            SettingUp();

            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);
            try
            {
                ecoScooter.People.Add(employee);

                ecoScooter.People.Add(new User(EXPECTED_USER_BIRTHDATE, EXPECTED_USER_DNI, EXPECTED_USER_EMAIL, EXPECTED_USER_NAME, EXPECTED_USER_TELEPHON,
                EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD));
            }
            catch (System.NullReferenceException) //Adding is not possible 
            {
                Assert.Fail("Collection of Person no properly initialized.\n");
            }

            dal.Insert(ecoScooter);
            dal.Commit();

            EcoScooter.Entities.EcoScooter ecoScooterDAL = dal.GetAll<EcoScooter.Entities.EcoScooter>().First();
            
            User userDAL = dal.GetAll<User>().First();
            int num_person = ecoScooterDAL.People.Count;
            Assert.AreEqual(EXPECTED_NUM_EXPLOYEES+EXPECTED_NUM_USERS, ecoScooterDAL.People.Count, "People is not stored");
            Assert.AreEqual(EXPECTED_DNI, ecoScooter.People.First().Dni, "Employee  not properly stored in People");
            Assert.AreEqual(EXPECTED_USER_DNI, ecoScooter.People.ElementAt(num_person-1).Dni, "USer  not properly stored in People");
            Assert.AreEqual(EXPECTED_USER_DNI, userDAL.Dni, "User not stored in the database");



        }
        [TestMethod]
        public void StoresScooters()
        {   //DAL and clear data
            SettingUp();
            Employee employee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(EXPECTED_DISCOUNT, EXPECTED_FARE, EXPECTED_SPEED, employee);
            try
            { 
                ecoScooter.Scooters.Add(new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE));
            }
            catch (System.NullReferenceException) //Adding is not possible 
            {
                Assert.Fail("Collection of Person no properly initialized.\n");
            }

            dal.Insert(ecoScooter);
            dal.Commit();

            EcoScooter.Entities.EcoScooter ecoScooterDAL = dal.GetAll<EcoScooter.Entities.EcoScooter>().First();
            Scooter scooterDAL = dal.GetAll<Scooter>().First();

            Assert.AreEqual(EXPECTED_NUM_SCOOTERS, ecoScooterDAL.Scooters.Count,"Scooters not properly stored in EcoScooter");
            Assert.AreEqual(EXPECTED_REGISTER_DATE, ecoScooterDAL.Scooters.First().RegisterDate, "Scooters not properly stored in EcoScooter");
            Assert.AreEqual(EXPECTED_REGISTER_DATE, scooterDAL.RegisterDate, "Scooter not properly stored in database");
            Assert.AreEqual(EXPECTED_STATE, scooterDAL.State, "Scooter not properly stored in database");

        }
        

    }
}
