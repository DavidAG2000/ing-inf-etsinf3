using System;
using System.Linq;
using EcoScooter.Entities;
using EcoScooter.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterPersistence
{
    [TestClass]
    public class UserPersistenceTest
    {   //User data
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
        //Rental Data
        private static DateTime? EXPECTED_END_DATE = new Date​Time(2019, 10, 14, 16, 55, 56);
        private static decimal EXPECTED_PRICE = 23;
        private static DateTime EXPECTED_START_DATE = new Date​Time(2019, 10, 14, 16, 15, 0);

        //Scooter Data
        private static DateTime EXPECTED_REGISTER_DATE = new Date​Time(2019, 10, 14);
        private const ScooterState EXPECTED_STATE = ScooterState.inMaintenance;
       
        
        // Station Data
        private const string EXPECTED_ADDRESS = "somewhere street";
        private const string EXPECTED_STATION_ID = "cityCenter";
        private const double EXPECTED_LAT = 10;
        private const double EXPECTED_LONG = 34;
        private static Station EXPECTED_STATION = new Station(EXPECTED_ADDRESS, EXPECTED_STATION_ID, EXPECTED_LAT, EXPECTED_LONG);

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

            User user = new User(EXPECTED_BIRTHDATE,EXPECTED_DNI,EXPECTED_EMAIL,EXPECTED_NAME,EXPECTED_TELEPHON,
                EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER,EXPECTED_PASSWORD);
            dal.Insert(user);
            dal.Commit();

            User userDal = dal.GetAll<User>().First();
            

            Assert.AreEqual(EXPECTED_BIRTHDATE, userDal.Birthdate, "Birthdate not properly stored");
            Assert.AreEqual(EXPECTED_DNI, userDal.Dni, "Dni  not properly stored");
            Assert.AreEqual(EXPECTED_EMAIL, userDal.Email, "Email not properly stored");
            Assert.AreEqual(EXPECTED_NAME, userDal.Name, "Name not properly storedr");
            Assert.AreEqual(EXPECTED_TELEPHON, userDal.Telephon, "Telephon not properly stored");
            Assert.AreEqual(EXPECTED_CVV, userDal.Cvv, "Cvv not properly storedt");
            Assert.AreEqual(EXPECTED_EXPIRATION_DATE, userDal.ExpirationDate, "ExperationDate not properly stored");
            Assert.AreEqual(EXPECTED_LOGIN, userDal.Login, "Login not properly stored");
            Assert.AreEqual(EXPECTED_NUMBER, userDal.Number, "Number not properly stored");
            Assert.AreEqual(EXPECTED_PASSWORD, userDal.Password, "Password not properly stored");

            Assert.IsNotNull(userDal.Rentals, "Collection of Rental no properly stored");

        }
        [TestMethod]
        public void stotesRental()
        {
            SettingUp();

            User user = new User(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
                EXPECTED_CVV, EXPECTED_EXPIRATION_DATE, EXPECTED_LOGIN, EXPECTED_NUMBER, EXPECTED_PASSWORD);
            Scooter expectedScooter = new Scooter(EXPECTED_REGISTER_DATE, EXPECTED_STATE);
            user.Rentals.Add(new Rental(EXPECTED_END_DATE, EXPECTED_PRICE, EXPECTED_START_DATE, EXPECTED_STATION, expectedScooter, user));
            dal.Insert(user);
            dal.Commit();

            User userDal = dal.GetAll<User>().First();
            Rental rentalDal = dal.GetAll<Rental>().First();
            Assert.AreEqual(user.Rentals.First().Id, userDal.Rentals.First().Id, "Rental of user no properly store");
            Assert.AreEqual(user.Rentals.First().Id, rentalDal.Id, "Rental of user no properly store  in Rental database");
            Assert.AreEqual(EXPECTED_DNI, rentalDal.User.Dni, "Rental of user no properly store in Rental database");

        }
    }
}
