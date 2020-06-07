using System;
using System.Data.Entity;
using System.Reflection;

namespace EcoScooter.Persistence
{
    public abstract class DbContextISW : DbContext
    {

        public DbContextISW(String s) : base(s) { }

        /// <summary>
        /// Delete every object stored in the DbSets
        /// Every class in the Namespace "...Entities" must have a DbSet in the DbContext
        /// </summary>
        public virtual void RemoveAllData()
        {
            foreach (Type t in Assembly.GetExecutingAssembly().GetTypes())
                if (t.FullName.Contains(".Entities"))
                    if (t.BaseType.FullName != "System.Enum") // NOT ALL TYPES HAVE A DBSET
                        Set(t).RemoveRange(Set(t));

            SaveChanges();
        }

    }
}
