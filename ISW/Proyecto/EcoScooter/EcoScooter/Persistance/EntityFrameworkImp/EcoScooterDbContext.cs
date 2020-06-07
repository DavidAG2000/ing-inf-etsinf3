using EcoScooter.Entities;
using System.Data.Entity;

namespace EcoScooter.Persistence
{
    public class EcoScooterDbContext : DbContextISW
    {
        public EcoScooterDbContext() : base("Name=EcoScooterDbConnection") //this is the connection string name
        {
            /*
            See DbContext.Configuration documentation
            */
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        static EcoScooterDbContext()
        {
            //Database.SetInitializer<BikeClubDbContext>(new CreateDatabaseIfNotExists<BikeClubDbContext>());
            Database.SetInitializer<EcoScooterDbContext>(new DropCreateDatabaseIfModelChanges<EcoScooterDbContext>());
            //Database.SetInitializer<BikeClubDbContext>(new DropCreateDatabaseAlways<BikeClubDbContext>());
            //Database.SetInitializer<BikeClubDbContext>(new BikeClubDbContextInitializer());
            //Database.SetInitializer(new NullDatabaseInitializer<BikeClubDbContext>());
        }

        // DbSets for persistent classes in your case study
        public IDbSet<Entities.EcoScooter> EcoScooters { get; set; }
        public IDbSet<Employee> Employees { get; set; }
        public IDbSet<Incident> Incidents { get; set; }
        public IDbSet<Maintenance> Maintenances { get; set; }
        public IDbSet<Person> Persons { get; set; }
        public IDbSet<PlanningWork> PlanningWorks { get; set; }
        public IDbSet<Rental> Rentals { get; set; }
        public IDbSet<Scooter> Scooters { get; set; }
        public IDbSet<Station> Stations { get; set; }
        public IDbSet<TrackPoint> TrackPoints { get; set; }
        public IDbSet<User> Users { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Primary keys with non conventional name

            modelBuilder.Entity<Person>().HasKey(p => p.Dni);
            modelBuilder.Entity<User>().HasKey(c => c.Dni);
            modelBuilder.Entity<Employee>().HasKey(c => c.Dni);
            //modelBuilder.Entity<CreditCard>().HasKey(c => c.Digits);

            // Classes with more than one relationship
            /*
            modelBuilder.Entity<Reservation>().HasRequired(r => r.PickUpOffice).WithMany(o => o.PickUpReservations).WillCascadeOnDelete(false);
            modelBuilder.Entity<Reservation>().HasRequired(r => r.ReturnOffice).WithMany(o => o.ReturnReservations).WillCascadeOnDelete(false);
            */
        }

        // Generic method to clear all the data (except some relations if needed)
        public override void RemoveAllData()
        {
            clearSomeRelationships();

            base.RemoveAllData();
        }

        // Sometimes it is needed to clear some relationships explicitly 
        private void clearSomeRelationships()
        {

        }

    }

}
