using System;
using System.Linq;
using EcoScooter.Entities;
using EcoScooter.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestEcoScooterPersistence
{
    [TestClass]
    public class PlanningWorkPersitenceTest
    {   //PLANINNG WORK DATA
        private const string EXPECTED_PLANNIG_DESCRIPTION = "Revise Battery";
        private const int EXPECTED_WORKING_TIME = 15;


        //EMPLOYEE DATA
        private static DateTime EXPECTED_BIRTHDATE = new Date​Time(2000, 07, 27);
        private const string EXPECTED_DNI = "14422178M";
        private const string EXPECTED_EMAIL = "somebody@outlook.es";
        private const string EXPECTED_NAME = "Somebody White";
        private const int EXPECTED_TELEPHON = 6006787;
        private const string EXPECTED_IBAN = "ES6621000418401234567891";
        private const int EXPECTED_PIN = 1234;
        private const string EXPECTED_POSITION = "manager";
        private const decimal EXPECTED_SALARY = 1250;
      
          //MAINTENANCE DATA
        private const string EXPECTED_DESCRIPTION = "Revise batteries";
        private static DateTime? EXPECTED_END_DATE = new Date​Time(2020, 1, 14);

        private static DateTime EXPECTED_START_DATE = new Date​Time(2019, 10, 14);


        //SCOOTER DATA
        //uses same registerData as StardDAte of Maintenance
        private static ScooterState EXPECTED_STATE = ScooterState.inMaintenance;

       

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
            Employee expectedEmployee = new Employee(EXPECTED_BIRTHDATE, EXPECTED_DNI, EXPECTED_EMAIL, EXPECTED_NAME, EXPECTED_TELEPHON,
               EXPECTED_IBAN, EXPECTED_PIN, EXPECTED_POSITION, EXPECTED_SALARY);
            Maintenance expectedMaintenance = new Maintenance(EXPECTED_DESCRIPTION, EXPECTED_END_DATE, EXPECTED_START_DATE, expectedEmployee);
            Scooter expectedScooter = new Scooter(EXPECTED_START_DATE, EXPECTED_STATE);
            PlanningWork planningWork = new PlanningWork(EXPECTED_PLANNIG_DESCRIPTION, EXPECTED_WORKING_TIME, expectedMaintenance, expectedScooter);
            dal.Insert(planningWork);
            dal.Commit();

            PlanningWork planningWorkDAL = dal.GetAll<PlanningWork>().First();
            Maintenance maintenanceDAL = dal.GetAll<Maintenance>().First();
            Scooter scooterDAL = dal.GetAll<Scooter>().First();

            Assert.AreEqual(EXPECTED_PLANNIG_DESCRIPTION, planningWorkDAL.Description, "Description of Planning Work no properly stored");
            Assert.AreEqual(EXPECTED_WORKING_TIME, planningWorkDAL.WorkingTime, "WorkingTime of Planning Work no properly stored");
            Assert.IsNotNull(planningWorkDAL.Maintenance, "Maintenance of Planning Work no properly stored");
            Assert.IsNotNull(planningWorkDAL.Scooter, "Scooter of Planning Work no properly stored");
            Assert.AreEqual(expectedScooter.Id, planningWorkDAL.Scooter.Id, "Scooter of Planning Work no properly stored");
            Assert.AreEqual(EXPECTED_DESCRIPTION, planningWorkDAL.Maintenance.Description, "Maintenance of Planning Work not properly stored in its table in the database·");     
            Assert.AreEqual(expectedScooter.Id, scooterDAL.Id, "Scooter of Planning Work not properly stored in its table in the database·");
            Assert.AreEqual(EXPECTED_DESCRIPTION, maintenanceDAL.Description, "Maintenance of Planning Work not properly stored in its table in the database·");

        }
       
    }
}
