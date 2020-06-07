using EcoScooter.Services;
using System;
using System.Collections.Generic;

namespace EcoScooter.Entities
{
    public partial class EcoScooter
    {

        public EcoScooter()
        {
            Employees = new HashSet<Employee>();
            People = new HashSet<Person>();
            Scooters = new HashSet<Scooter>();
            Stations = new HashSet<Station>();
            // Empleat de prova
            //Employee test = new Employee(DateTime.Now, "12345678", "abc@abc.com", "Jesucristo", 657991932, "ES1234", 1234, "Rey", 1000);
            //People.Add(test);
            //Employees.Add(test);
        }

        public EcoScooter(double discountYonger, double fare, double maxSpeed, Employee employee) : this()
        {
            DiscountYounger = discountYonger;
            Fare = fare;
            MaxSpeed = maxSpeed;
            People.Add(employee);
            Employees.Add(employee);
        }

        /* Comproba que el nou usuari ha introduït una tarjeta de credit valida, la tarjeta caducada,
         * no és menor (16 anys), no existeix una persona amb el mateix DNI ni hi ha ja un User amb eixe login */
        public void CheckRegister(DateTime expirationDate, int number, string login, DateTime birthDate, string dni)
        {
            if (number.ToString().Length != 8)
            {
                throw new ServiceException("ERROR: Longitud de la tarjeta incorrecta. Ha de ser de 8 nombres");
            }
            else if (!CheckDate(expirationDate))
            {
                throw new ServiceException("ERROR: La tarjeta està caducada");
            }
            else if (IsMinor(birthDate))
            {
                throw new ServiceException("ERROR: L'edat minima per a registrar-se és de 16 anys");
            }
            else
            {
                foreach (Person person in People)
                {
                    if (person.Dni.Trim().Equals(dni))
                    {
                        throw new ServiceException("ERROR: Ja hi ha una persona registrada amb eixe DNI");
                    }
                    if (typeof(User).IsInstanceOfType(person))
                    {
                        User user = ((User)person);
                        if (login.Trim().Equals(user.Login))
                        {
                            throw new ServiceException("ERROR: Ja hi ha un usuari amb eixe login");
                        }
                    }
                }
            }
        }

        internal void CheckNoEmpty(string email, string password, string dni, string name, string login)
        {
            if (email.Trim().Length == 0)
            {
                throw new ServiceException("ERROR: El camp email està buit");
            }
            if (!email.Trim().Contains("@"))
            {
                throw new ServiceException("ERROR: Format de email no vàlid");
            }
            if (password.Length < 4)
            {
                throw new ServiceException("ERROR: La contrassenya ha de tindre almenys 4 caracters");
            }
            if (dni.Trim().Length != 9)
            {
                throw new ServiceException("ERROR: El DNI ha de tindre 9 caracters");
            }
            if (int.TryParse(dni.Trim().Substring(dni.Trim().Length - 1), out int a))
            {
                throw new ServiceException("ERROR: L'últim caracter del DNI ha de ser una lletra");
            }
            if (name.Trim().Length == 0)
            {
                throw new ServiceException("ERROR: El camp nom està buit");
            }
            if (login.Length < 4)
            {
                throw new ServiceException("ERROR: El login ha de tindre almenys 4 caracters");
            }
        }

        /* Comproba que donada la data de naixement d'una persona, aquesta és o no menor (16 anys) */
        public bool IsMinor(DateTime birthDate)
        {
            DateTime now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day))
                age--;

            return age < 16;
        }

        /* Comproba que donada una data, el succes és o no posterior a ara */
        public bool CheckDate(DateTime expirationDate)
        {
            return expirationDate.CompareTo(DateTime.Now) > 0;
        }

        /* Comproba que tots el parametres per acrear una nova estació siguen correctes */
        public void CheckStation(string address, double latitude, double longitude, string id)
        {
            if (address.Trim().Equals("")) { throw new ServiceException("ERROR: Adreça incorrecta"); }
            if (latitude < -90.0 || latitude > 90.0) { throw new ServiceException("ERROR: Latitud inadequada"); }
            if (longitude < -180.0 || longitude > 180.0) { throw new ServiceException("ERROR: Longitut inadequada"); }
            if (id.Trim().Equals("")) { throw new ServiceException("ERROR: L'identificador no pot estar buit"); }
            foreach (Station s in Stations)
            {
                if (address.Equals(s.Address)) { throw new ServiceException("ERROR: Ja existeix una estació amb eixa adreça"); }
                if (latitude == s.Latitude && longitude == s.Longitude) { throw new ServiceException("ERROR: Ja existeix una estació en eixa localització"); }
                if (id.Equals(s.Id)) { throw new ServiceException("ERROR: ja existeix una estació amb eixe identificador"); }
            }
        }

        /* Obte el usuari solicitat, si existeix */
        public User GetUser(string login)
        {
            foreach (Person person in People)
            {
                if (typeof(User).IsInstanceOfType(person))
                {
                    User aux = ((User)person);
                    if (aux.Login.Equals(login))
                    {
                        return aux;
                    }
                }
            }
            throw new ServiceException("ERROR: No hi ha cap usuari amb eixe login");
        }

        /* Obte el empleat solicitat, si existeix */
        public Employee GetEmployee(string dni)
        {
            foreach (Employee person in Employees)
            {
                if (person.Dni.Equals(dni))
                {
                    return person;
                }
            }
            throw new ServiceException("ERROR: No hi ha cap empleat amb eixe DNI");
        }

        /* Obte l'estació solicitada, si existeix */
        public Station GetStation(string id)
        {
            foreach (Station station in Stations)
            {
                if (station.Id.Equals(id))
                {
                    return station;
                }
            }
            throw new ServiceException("ERROR: No hi ha cap estació amb eixe id");
        }


    }
}
