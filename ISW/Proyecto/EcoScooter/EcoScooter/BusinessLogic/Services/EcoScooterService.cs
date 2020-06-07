using EcoScooter.Entities;
using EcoScooter.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoScooter.Services
{
    public class EcoScooterService : IEcoScooterService
    {
        private readonly IDAL dal;
        private readonly EcoScooter.Entities.EcoScooter ecoScooter;
        //Hay que mantener una referencia al usuario con la sesión actualmente iniciada. Se debe declarar bajo esta línea.
        private Person loggedPerson = null;

        public EcoScooterService(IDAL dal)
        {
            this.dal = dal;
            try
            {
                ecoScooter = dal.GetAll<EcoScooter.Entities.EcoScooter>().First();
            }
            catch (InvalidOperationException)
            {
                ecoScooter = new Entities.EcoScooter();
                UpdateEcoScooterData(10, 15, 30); //15 en lugar de 0.015 para una simulación más conveniente.
                dal.Insert<EcoScooter.Entities.EcoScooter>(ecoScooter);
                saveChanges();
            }
        }

        /// <summary>
        /// Updates fare, discount and maxspeed of the system 
        /// </summary>
        /// <param name="discountYounger"></param>
        /// <param name="fare"></param>
        /// <param name="maxSpeed"></param>
        public void UpdateEcoScooterData(double discountYounger, double fare, double maxSpeed)
        {
            ecoScooter.DiscountYounger = discountYounger;
            ecoScooter.Fare = fare;
            ecoScooter.MaxSpeed = maxSpeed;

        }

        public void removeAllData()
        {
            dal.Clear<Entities.EcoScooter>();
            dal.Clear<Employee>();
            dal.Clear<Incident>();
            dal.Clear<Person>();
            dal.Clear<PlanningWork>();
            dal.Clear<Rental>();
            dal.Clear<Scooter>();
            dal.Clear<Station>();
            dal.Clear<TrackPoint>();
            dal.Clear<User>();
            saveChanges();
        }

        public void saveChanges()
        {
            dal.Commit();
        }

        #region Cas 1: Registrar-se
        public void RegisterUser(DateTime birthDate, string dni, string email, string name, int telephon, int cvv, DateTime expirationDate, string login, int number, string password)
        {
            ecoScooter.CheckNoEmpty(email, password, dni, name, login);
            ecoScooter.CheckRegister(expirationDate, number, login, birthDate, dni);
            User user = new User(birthDate, dni, email, name, telephon, cvv, expirationDate, login, number, password);
            ecoScooter.People.Add(user);
            saveChanges();
        }
        #endregion

        #region Cas 2: Logins
        public void LoginEmployee(string dni, int pin)
        {
            if (loggedPerson != null)
            {
                throw new ServiceException("ERROR: Ja hi ha una persona logged");
            }

            Employee employee = ecoScooter.GetEmployee(dni);
            if (employee.Pin == pin)
            {
                loggedPerson = employee;
            }
            else
            {
                throw new ServiceException("ERROR: El pin introduït és incorrecte");
            }
        }

        public void LoginUser(string login, string password)
        {
            if (loggedPerson != null)
            {
                throw new ServiceException("ERROR: Ja hi ha una persona logged");
            }

            User user = ecoScooter.GetUser(login);
            if (user.Password.Equals(password))
            {
                loggedPerson = user;
            }
            else
            {
                throw new ServiceException("ERROR: La contrasenya introduïda és incorrecta");
            }
        }

        public bool isLoggedAsEmployee(string dni)
        {
            if (loggedPerson != null && loggedPerson.Dni.Equals(dni)
                && typeof(Employee).IsInstanceOfType(loggedPerson))
            {
                return true;
            }
            else
            {
                throw new ServiceException("ERROR: Acces denegat. La funció correspon a un empleat");
            }
        }

        public bool isLoggedAsUser(string dni)
        {
            if (loggedPerson != null && loggedPerson.Dni.Equals(dni)
                && typeof(User).IsInstanceOfType(loggedPerson))
            {
                return true;
            }
            else
            {
                throw new ServiceException("ERROR: Acces denegat. La funció correspon a un usuari");
            }
        }
        #endregion

        #region Cas 3: Registrar estació
        public void RegisterStation(string address, double latitude, double longitude, string stationId)
        {
            /* Precondicio */
            if (loggedPerson == null || !isLoggedAsEmployee(loggedPerson.Dni))
            {
                throw new ServiceException("ERROR: No hi ha cap persona logged");
            }

            ecoScooter.CheckStation(address, latitude, longitude, stationId);
            Station s = new Station(address, stationId, latitude, longitude);
            ecoScooter.Stations.Add(s);
            saveChanges();
        }
        #endregion

        #region Cas 4: Registrar patinet
        public void RegisterScooter(DateTime registerDate, ScooterState state, String stationId)
        {
            /* Precondicio */
            if (loggedPerson == null || !isLoggedAsEmployee(loggedPerson.Dni))
            {
                throw new ServiceException("ERROR: No hi ha cap persona logged");
            }
            if (ecoScooter.CheckDate(registerDate))
            {
                throw new ServiceException("ERROR: La data de registre no pot ser en el futur");
            }
            Scooter s = new Scooter(registerDate, state);
            if (state == ScooterState.available)
            {
                Station e = ecoScooter.GetStation(stationId);
                s.Station = e;
                e.Scooters.Add(s);
            }

            ecoScooter.Scooters.Add(s);
            saveChanges();
        }
        #endregion

        #region Cas 5: Llogar patinet
        public void RentScooter(string stationId)
        {
            /* Precondicio */
            if (loggedPerson == null || !isLoggedAsUser(loggedPerson.Dni))
            {
                throw new ServiceException("ERROR: No hi ha cap persona logged");
            }
            if (((User)loggedPerson).Rentals.Count > 0 && ((User)loggedPerson).Rentals.Last().DestinationStation == null)
            {
                throw new ServiceException("ERROR: Hi ha un lloguer pendent encara");
            }
            /* Pas 1 */
            Station station = ecoScooter.GetStation(stationId);
            /* Pas 2 & 3 */
            Scooter scooter = station.GetScooter();
            scooter.Station = null;
            scooter.State = ScooterState.inUse;
            /* Pas 4 */
            Rental rental = new Rental(DateTime.Now, 0,
                DateTime.Now, station, scooter, ((User)loggedPerson));
            ((User)loggedPerson).Rentals.Add(rental);
            scooter.Rentals.Add(rental);
            station.OriginRentals.Add(rental);
            /* Postcondició */
            saveChanges();
        }
        #endregion

        #region Cas 6: Tornar patinet
        public void ReturnScooter(string stationId)
        {
            /* Precondicio */
            if (loggedPerson == null || !isLoggedAsUser(loggedPerson.Dni))
            {
                throw new ServiceException("ERROR: No hi ha cap persona logged");
            }
            /* Pas 2 */
            if (((User)loggedPerson).Rentals.Count == 0)
            {
                throw new ServiceException("ERROR: No hi cap alquiler");
            }
            Rental rental = ((User)loggedPerson).Rentals.Last<Rental>();
            if (rental.DestinationStation != null)
            {
                throw new ServiceException("ERROR: No hi cap alquiler pendent");
            }
            /* Pas 1 */
            Station station = ecoScooter.GetStation(stationId);
            /* Pas 3 & 4*/
            // S'en carrega la capa UI
            /* Pas 5 */
            rental.EndDate = DateTime.Now;
            rental.DestinationStation = station;
            rental.Scooter.State = ScooterState.available;
            rental.Scooter.Station = station;
            TimeSpan aux = ((DateTime)rental.EndDate) - rental.StartDate;
            if (loggedPerson.IsYoung())
            {
                rental.Price = Convert.ToDecimal(ecoScooter.Fare * aux.TotalMinutes * 0.9);
            }
            else
            {
                rental.Price = Convert.ToDecimal((ecoScooter.Fare * aux.TotalMinutes));
            }
            station.Scooters.Add(rental.Scooter);
            station.DestinationRentals.Add(rental);
            /* Postcondició */
            saveChanges();
        }
        #endregion

        #region Cas 7: Registrar incident
        public void RegisterIncident(string description, DateTime timeStamp, int rentalId)
        {
            /* Precondicio */
            if (loggedPerson == null || !isLoggedAsUser(loggedPerson.Dni))
            {
                throw new ServiceException("ERROR: No hi ha cap persona logged");
            }
            /*Pas 1*/
            Incident i = new Incident(description, timeStamp);
            /*Pas 2*/
            Rental rental = dal.GetById<Rental>(rentalId);
            rental.Incident = i;
            saveChanges();
        }
        #endregion

        #region Cas 8: Obtenir recorreguts

        public ICollection<string> GetUserRoutesIds(DateTime startDate, DateTime endDate)
        {
            /* Precondicio */
            if (loggedPerson == null || !isLoggedAsUser(loggedPerson.Dni))
            {
                throw new ServiceException("ERROR: No hi ha cap persona logged");
            }
            /*obtenim les rentals*/
            ICollection<string> res = new List<string>();
            /*De elles escollim les que estan en el interval desitjat, anyadint la id a la llista res*/
            foreach (Rental aux in ((User)loggedPerson).Rentals)
            {
                if (aux.StartDate.CompareTo(startDate) >= 0 && ((DateTime)aux.EndDate).CompareTo(endDate) <= 0)
                {
                    res.Add(aux.Id.ToString());
                }
            }
            if (res.Count == 0)
            {
                throw new ServiceException("AVIS: No hi ha cap prestec en eixe periode");
            }
            return res;
        }

        public ICollection<string> GetUserRoutes(DateTime startDate, DateTime endDate)
        {
            /* Precondicio */
            if (loggedPerson == null || !isLoggedAsUser(loggedPerson.Dni))
            {
                throw new ServiceException("ERROR: No hi ha cap persona logged");
            }
            /* Fem la crida per obtindre les id dels rentals del user */
            ICollection<string> llistaIds = GetUserRoutesIds(startDate, endDate);
            /*recorregem les ids tranguent la info necessaria*/
            ICollection<string> res = new List<string>();
            foreach (String aux in llistaIds)
            {
                Rental rental = ((User)loggedPerson).GetRental(Int32.Parse(aux));
                if (rental.DestinationStation != null)
                {
                    res.Add(rental.OriginStation.Id + ", " + rental.DestinationStation.Id + ", " + rental.StartDate + ", " + rental.EndDate + ", " + rental.Price);
                }
                /*afegim la info de la rental, l'estacio de desti es lultim trackpoint*/
            }

            return res;
        }

        public void GetRouteDescription(int rentalId, out DateTime startDate, out DateTime endDate, out decimal price, out String originStationId, out String destinationStationId)
        {
            /* Precondicio */
            if (loggedPerson == null || !isLoggedAsUser(loggedPerson.Dni))
            {
                throw new ServiceException("ERROR: No hi ha cap persona logged");
            }
            Rental rental = dal.GetById<Rental>(rentalId);
            startDate = rental.StartDate;
            endDate = ((DateTime)rental.EndDate);
            price = rental.Price;
            originStationId = rental.OriginStation.Id;
            destinationStationId = rental.DestinationStation.Id;
        }
        #endregion

        public void Disconnect()
        {
            loggedPerson = null;
        }

        public int LastRentalId()
        {
            return ((User)loggedPerson).Rentals.Last().Id;
        }
    }
}