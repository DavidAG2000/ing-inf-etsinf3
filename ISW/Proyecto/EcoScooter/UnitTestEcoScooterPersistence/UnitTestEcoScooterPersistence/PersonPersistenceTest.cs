using System;
using System.Linq;
using EcoScooter.Entities;
using EcoScooter.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterPersistence
{

    [TestClass]
    public class PersonPersistenceTest
    {   //PERSON DATA
        private static DateTime EXPECTED_BIRTHDATE = new Date​Time(2000, 07, 27);
        private const string EXPECTED_DNI = "14422178M";
        private const string EXPECTED_EMAIL = "somebody@outlook.es";
        private const string EXPECTED_NAME = "Somebody White";
        private const int EXPECTED_TELEPHON = 6006787;

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
            Person person = new Person(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON);
            dal.Insert(person);
            dal.Commit();

            Person personDAL = dal.GetAll<Person>().First();

            Assert.AreEqual(EXPECTED_BIRTHDATE, personDAL.Birthdate, "Birthdate of Person no properly stored");
            Assert.AreEqual(EXPECTED_DNI, personDAL.Dni, "Dni  of Person no properly stored");
            Assert.AreEqual(EXPECTED_EMAIL, personDAL.Email, "Email  of Person no properly stored");
            Assert.AreEqual(EXPECTED_NAME, personDAL.Name, "Name  of Person no properly stored");
            Assert.AreEqual(EXPECTED_TELEPHON, personDAL.Telephon, "Telephon of Person no properly stored");


        }
    }
}
