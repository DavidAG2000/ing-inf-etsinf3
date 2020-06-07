using System;
using System.Collections.Generic;

namespace EcoScooter.Entities
{
    public partial class Scooter
    {
        public Scooter()
        {
            PlanningWork = new List<PlanningWork>();
            Rentals = new List<Rental>();
        }
        public Scooter(DateTime registerDate, ScooterState state) : this()
        {
            RegisterDate = registerDate;
            State = state;
        }
    }
}
