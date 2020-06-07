using EcoScooter.Entities;
using EcoScooter.Persistence;
using System;
using System.Linq;

namespace UnitTestServiceEcoScooter
{
    class Populate
    {

        private static void AddEcoScooter(IDAL dal)
        {

            Employee employee = new Employee(new Date​Time(1995, 07, 27), "12345678Z", "first@employee.es", "First Employe", 600123456, "ES6621000418401234567891", 123, "Maintenance", 1200);
            EcoScooter.Entities.EcoScooter ecoScooter = new EcoScooter.Entities.EcoScooter(10, 15, 30, employee);
            ecoScooter.People.Add(employee);
            dal.Insert(ecoScooter);
            dal.Commit();
        }
        private static void AddUsers(IDAL dal)
        {
            EcoScooter.Entities.EcoScooter ecoScooter = dal.GetAll<EcoScooter.Entities.EcoScooter>().First();
            User user1 = new User(new Date​Time(2000, 12, 01), "12345679S", "first@user.es", "First User", 605123456, 345, new Date​Time(2022, 12, 31), "firstUSer", 12345678, "1234");

            User user2 = new User(new Date​Time(1977, 12, 01), "12345680Q", "second@user.es", "Second User", 605123457, 543, new Date​Time(2023, 12, 31), "secondtUSer", 87654321, "4321");
            dal.Insert(user1);
            dal.Insert(user2);
            ecoScooter.People.Add(user1);
            ecoScooter.People.Add(user2);
            dal.Commit();

        }
        private static void AddStations(IDAL dal)
        {
            EcoScooter.Entities.EcoScooter ecoScooter = dal.GetAll<EcoScooter.Entities.EcoScooter>().First();
            ecoScooter.Stations.Add(new Station("Camino de Vera s/n. 46022 Valencia", "UPV", 39.482369, -0.343578));
            ecoScooter.Stations.Add(new Station("Avda Aragón 33. 46010 Valencia", "Aragón", 39.4698, -0.3774));
            dal.Commit();

        }
        private static void AddScooters(IDAL dal)
        {
            EcoScooter.Entities.EcoScooter ecoScooter = dal.GetAll<EcoScooter.Entities.EcoScooter>().First();
            Scooter scooter1 = new Scooter(new Date​Time(2019, 01, 01), ScooterState.available);
            scooter1.Station = ecoScooter.Stations.First();
            ecoScooter.Scooters.Add(scooter1);
            ecoScooter.Scooters.Add(new Scooter(new Date​Time(2019, 01, 01), ScooterState.inMaintenance));
            dal.Commit();

        }
        private static void AddRentals(IDAL dal)
        {
            EcoScooter.Entities.EcoScooter ecoScooter = dal.GetAll<EcoScooter.Entities.EcoScooter>().First();

            var users = ecoScooter.People.OfType<User>();
            Rental rental = new Rental(new Date​Time(2019, 10, 14, 16, 55, 56), 30, new Date​Time(2019, 10, 14, 16, 54, 56),
            ecoScooter.Stations.First(), ecoScooter.Scooters.First(), users.First());
            rental.DestinationStation = ecoScooter.Stations.Last();
            dal.Insert(rental);
            dal.Insert(new Rental(null, 0, new Date​Time(2019, 11, 06, 12, 00, 00),
            ecoScooter.Stations.First(), ecoScooter.Scooters.First(), users.First()));
            dal.Commit();

        }
        //Rental rental = new Rental(new Date​Time(2019, 10, 14, 16, 55, 56),30, new Date​Time(2019, 10, 14, 16, 54, 56));
        public static void PopulateDB()
        {

            IDAL dal = new EntityFrameworkDAL(new EcoScooterDbContext());
            dal.RemoveAllData();
            AddEcoScooter(dal);
            AddUsers(dal);
            AddStations(dal);
            AddScooters(dal);
            AddRentals(dal);

        }
    }
}
