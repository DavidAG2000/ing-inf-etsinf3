using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoScooter.Entities
{
    public partial class Scooter
    {
        [Key]
        public int Id
        {
            get;
            set;
        }
        public DateTime RegisterDate
        {
            get;
            set;
        }
        public ScooterState State
        {
            get;
            set;
        }
        /*Relacions*/
        public virtual Maintenance Maintenance
        {
            get;
            set;
        }
        public virtual ICollection<PlanningWork> PlanningWork
        {
            get;
            set;
        }
        public virtual ICollection<Rental> Rentals
        {
            get;
            set;
        }
        [InverseProperty("Scooters")]
        public virtual Station Station
        {
            get;
            set;
        }
    }
}
