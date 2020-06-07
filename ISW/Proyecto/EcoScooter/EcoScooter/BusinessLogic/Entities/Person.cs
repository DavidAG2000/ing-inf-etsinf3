using System;

namespace EcoScooter.Entities
{
    public partial class Person
    {
        public Person()
        {
        }


        public Person(DateTime birthDate, string dni, string email, string name, int telephon)
        {
            Birthdate = birthDate;
            Email = email;
            Name = name;
            Telephon = telephon;
            Dni = dni;
        }

        /* Comproba si una persona és jove (major de 16 i menor de 25) */
        public bool IsYoung()
        {
            DateTime now = DateTime.Now;
            int age = now.Year - Birthdate.Year;
            if (now.Month < Birthdate.Month || (now.Month == Birthdate.Month && now.Day < Birthdate.Day))
                age--;

            return age >= 16 && age < 25;
        }
    }
}
