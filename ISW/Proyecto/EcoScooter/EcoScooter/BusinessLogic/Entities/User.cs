using EcoScooter.Services;
using System;
using System.Collections.Generic;

namespace EcoScooter.Entities
{
    public partial class User : Person
    {
        public User()
        {
            Rentals = new HashSet<Rental>();
        }


        public User(DateTime birthDate, string dni, string email, string name, int telephon,
            int cvv, DateTime expirationDate, string login, int number, string password) : base(birthDate, dni, email, name, telephon)
        {
            Cvv = cvv;
            ExpirationDate = expirationDate;
            Login = login;
            Number = number;
            Password = password;
            Rentals = new List<Rental>();
        }

        /* Obte el rental selecionat d'un usuari */
        public Rental GetRental(int id)
        {
            foreach (Rental rental in Rentals)
            {
                if (rental.Id == id)
                {
                    return rental;
                }
            }
            throw new ServiceException("ERROR: L'usuari no te cap lloguer amb eixe ID");
        }
    }
}
