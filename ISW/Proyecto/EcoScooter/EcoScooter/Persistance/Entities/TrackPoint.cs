using System;
using System.ComponentModel.DataAnnotations;

namespace EcoScooter.Entities
{
    public partial class TrackPoint
    {
        [Key]
        public int Id
        {
            get;
            set;
        }
        public Double BatteryLevel
        {
            get;
            set;
        }

        public Double Latitude
        {
            get;
            set;
        }

        public Double Longitude
        {
            get;
            set;
        }

        public Double Speed
        {
            get;
            set;
        }

        public DateTime? Timestamp
        {
            get;
            set;
        }
    }
}
