using System;
using System.Collections.Generic;

namespace EcoScooter.Entities
{
    public partial class Maintenance
    {
        public Maintenance()
        {
            PlanningWork = new List<PlanningWork>();
        }

        public Maintenance(String description, DateTime? endDate, DateTime startDate, Employee employee) : this()
        {
            Description = description;
            EndDate = endDate;
            Employee = employee;
            StartDate = startDate;
        }
    }
}
