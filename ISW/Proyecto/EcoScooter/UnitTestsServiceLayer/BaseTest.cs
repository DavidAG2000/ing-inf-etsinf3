using EcoScooter.Persistence;
using EcoScooter.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestServiceEcoScooter
{
    [TestClass]
    public class BaseTest
    {
        protected private EntityFrameworkDAL dal;
        protected private EcoScooterService ecoScooterService;
        [TestInitialize]
        public void IniTests()
        {
            dal = new EntityFrameworkDAL(new EcoScooterDbContext());
            dal.RemoveAllData();


        }
        [TestCleanup]
        public void CleanTests()
        {
            dal.RemoveAllData();
        }
    }
}
