using System;
using System.Collections.Generic;

namespace EcoScooter.Entities
{
    public partial class User : Person
    {
        public int Cvv
        {
            get;
            set;
        }

        public DateTime ExpirationDate
        {
            get;
            set;
        }

        public String Login
        {
            get;
            set;
        }

        public int Number
        {
            get;
            set;
        }

        public String Password
        {
            get;
            set;
        }

        public virtual ICollection<Rental> Rentals
        {
            get;
            set;
        }
    }
}
