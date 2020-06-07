using System;
using System.Collections.Generic;

namespace EcoScooter.Entities
{
    public partial class Employee : Person
    {

        public string Iban
        {
            get;
            set;
        }
        public int Pin
        {
            get;
            set;
        }
        public string Position
        {
            get;
            set;
        }
        public Decimal Salary
        {
            get;
            set;
        }
        /*Relaciones*/
        public virtual ICollection<Maintenance> Maintenances
        {
            get;
            set;
        }
    }
}