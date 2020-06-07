using EcoScooter.Services;
using System.Collections.Generic;
using System.Linq;

namespace EcoScooter.Entities
{
    public partial class Station
    {

        public Station()
        {
            DestinationRentals = new List<Rental>();
            OriginRentals = new List<Rental>();
            Scooters = new HashSet<Scooter>();
        }

        public Station(string address, string id, double latitude, double longitude) : this()
        {
            Address = address;
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }

        /* Obte, si hi ha, un patinet lliure de l'estació */
        public Scooter GetScooter()
        {
            if (Scooters.Count > 0)
            {
                Scooter res = Scooters.ElementAt<Scooter>(0);
                Scooters.ToList<Scooter>().RemoveAt(0);
                return res;
            }
            else
            {
                throw new ServiceException("ERROR: No queden patinets lliures a la estació");
            }
        }
    }
}
