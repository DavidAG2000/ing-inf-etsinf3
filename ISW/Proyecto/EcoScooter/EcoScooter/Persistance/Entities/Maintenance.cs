using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoScooter.Entities
{
    public partial class Maintenance
    {

        public string Description
        {
            get;
            set;
        }
        public DateTime? EndDate
        {
            get;
            set;
        }
        [Key]
        public int Id
        {
            get;
            set;
        }
        public DateTime StartDate
        {
            get;
            set;
        }
        /*Relaciones*/
        [InverseProperty("Maintenances")]
        public virtual Employee Employee
        {
            get;
            set;
        }
        public virtual ICollection<PlanningWork> PlanningWork
        {
            get;
            set;
        }
    }
}