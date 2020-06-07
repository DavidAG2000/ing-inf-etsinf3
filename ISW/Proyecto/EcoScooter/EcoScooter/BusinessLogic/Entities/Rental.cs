using EcoScooter.Services;
using System;
using System.Collections.Generic;

namespace EcoScooter.Entities
{
    public partial class Rental
    {
        public Rental()
        {
            TrackPoints = new List<TrackPoint>();
        }

        public Rental(DateTime? endDate, Decimal price, DateTime startDate,
            Station originStation, Scooter scooter, User user) : this()
        {
            EndDate = endDate;
            Price = price;
            StartDate = startDate;
            OriginStation = originStation;
            Scooter = scooter;
            User = user;
        }
    }
}
