using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoScooter.Entities
{
    public partial class PlanningWork
    {
        [Key]
        public int Id
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public int WorkingTime
        {
            get;
            set;
        }
        /*Relaciones*/
        [InverseProperty("PlanningWork")]
        public virtual Maintenance Maintenance
        {
            get;
            set;
        }
        [InverseProperty("PlanningWork")]
        public virtual Scooter Scooter
        {
            get;
            set;
        }
    }
}
