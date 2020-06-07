using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcoScooter.Entities
{
    public partial class Station
    {

        public string Address
        {
            get;
            set;
        }

        [Key]
        public string Id
        {
            get;
            set;
        }

        public double Latitude
        {
            get;
            set;
        }

        public double Longitude
        {
            get;
            set;
        }

        public virtual ICollection<Rental> DestinationRentals
        {
            get;
            set;
        }

        public virtual ICollection<Rental> OriginRentals
        {
            get;
            set;
        }

        public virtual ICollection<Scooter> Scooters
        {
            get;
            set;
        }

    }
}
