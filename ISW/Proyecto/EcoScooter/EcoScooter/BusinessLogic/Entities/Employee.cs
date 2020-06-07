using System;
using System.Collections.Generic;

namespace EcoScooter.Entities
{
    public partial class Employee : Person
    {
        public Employee() : base()
        {
            Maintenances = new List<Maintenance>();
        }

        public Employee(DateTime birthDate, string dni, string email, String
       name, int telephon, string iban, int pin, string position, Decimal salary) :
       base(birthDate, dni, email, name, telephon)
        {
            Iban = iban;
            Pin = pin;
            Position = position;
            Salary = salary;
            Maintenances = new List<Maintenance>();
        }
    }
}
