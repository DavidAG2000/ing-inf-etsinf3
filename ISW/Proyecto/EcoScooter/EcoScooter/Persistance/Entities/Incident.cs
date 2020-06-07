using System;
using System.ComponentModel.DataAnnotations;

namespace EcoScooter.Entities
{
    public partial class Incident
    {
        public String Description
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

        public DateTime? TimeStamp
        {
            get;
            set;
        }
    }
}
