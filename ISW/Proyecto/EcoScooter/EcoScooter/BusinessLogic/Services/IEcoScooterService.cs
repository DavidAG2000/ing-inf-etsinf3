using EcoScooter.Entities;
using System;
using System.Collections.Generic;

namespace EcoScooter.Services
{
    public interface IEcoScooterService
    {
        /// <summary>
        /// Removes all data from the database.
        /// </summary>
        void removeAllData();

        /// <summary>
        /// Saves all modified entities.
        /// </summary>
        void saveChanges();

        /// <summary>
        /// Returns true if the user is the current logged user, false otherwise
        /// </summary>
        /// <param name="dni"></param>
        /// <returns></returns>
        ///  <exception cref="ServiceException">Thrown when user's data is incorrrect</exception>
        bool isLoggedAsUser(string dni);

        /// <summary>
        /// Returns true if the employee is the current logged user, false otherwise
        /// </summary>
        /// <param name="dni"></param>
        /// <returns></returns>
        ///  <exception cref="ServiceException">Thrown when employee's data is incorrrect</exception>
        bool isLoggedAsEmployee(string dni);


        /// <summary>
        /// Registers a new user into the EcoScooter System. 
        /// </summary>
        /// <param name="birthDate"></param>
        /// <param name="dni"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="telephon"></param>
        /// <param name="cvv"></param>
        /// <param name="expirationDate"></param>
        /// <param name="login"></param>
        /// <param name="number"></param>
        /// <param name="password"></param>
        /// <exception cref="ServiceException">Thrown when user's data is incorrrect or the dni already exists</exception> 
        void RegisterUser(DateTime birthDate, String dni, String email, String name, int telephon, int cvv, DateTime expirationDate, string login, int number, string password);

        /// <summary>
        /// Allows user login the system. 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <exception cref="ServiceException">Thrown when user's data is incorrrect or the user doesn't exist </exception> 
        void LoginUser(string login, string password);

        /// <summary>
        ///  Allows employee login the system. 
        /// </summary>
        /// <param name="dni"></param>
        /// <param name="pin"></param>
        ///  <exception cref="ServiceException">Thrown when employee's data is incorrrect or the employee doesn't exist  </exception> 
        void LoginEmployee(String dni, int pin);

        /// <summary>
        /// Registers a new sttion into the system
        /// </summary>
        /// <param name="address"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="">stationId</param>
        /// <exception cref="ServiceException">Thrown when the input data is incorrect or the stationId already exists in the system</exception>
        void RegisterStation(String address, Double latitude, Double longitude, String stationId);

        /// <summary>
        /// Registers a new scooter into the system. If ScooterState is "available", furthermore, it assigns the scooter to the station provided. 
        /// </summary>
        /// <param name="registerDate"></param>
        /// <param name="state"></param>
        /// <param name="stationId"></param>
        ///  <exception cref="ServiceException">Thrown when the input data is incorrect: wrong registerDate, incorrect state or not existing station  </exception> 
        void RegisterScooter(DateTime registerDate, ScooterState state, String stationId);

        /// <summary>
        /// Rents a scooter from one station
        /// </summary>
        /// <param name="stationId"></param>
        /// <exception cref="ServiceException">Thrown when the station doesn't exist or there isn't an available scooter in the station </exception> 
        void RentScooter(string stationId);

        /// <summary>
        /// Returns a scooter in rental. The scooter state and the rental price is updated.
        /// </summary>
        /// <param name="stationId"></param>
        /// <exception cref="ServiceException">Thrown when the station doesn't exist or if the last rental already was returned</exception> 
        void ReturnScooter(string stationId);

        /// <summary>
        /// Register a new incident associated to a rental
        /// </summary>
        /// <param name="description"></param>
        /// <param name="timeStamp"></param>
        /// <param name="rentalId"></param>
        ///  <exception cref="ServiceException">Thrown when the timeStamp is not within the interval marked between the start and end date of the rental provided
        /// or the rental doe </exception> 
        void RegisterIncident(string description, DateTime timeStamp, int rentalId);


        /// <summary>
        /// Gets the ids of the rentals corresponding to the user's routes  made within the date interval provided. 
        /// The route description is a string with the following fields splitted by commas: startDate, endDate, price, Origin stationId, Destination stationID
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <exception cref="ServiceException"> Thrown when the interval marked between the start and end date is wrong 
        /// or there are not available routes in that period </exception> 
        ICollection<String> GetUserRoutesIds(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets the descriptions of the user's routes associated to the rentalId provided: startDate, endDate, price, Origin stationId, Destination stationID
        /// </summary>
        /// <param name="rentalId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="price"></param>
        /// <param name="originStationId"></param>
        /// <param name="destinationStationId"></param>
        /// <exception cref="ServiceException"> Thrown when the there is not a rental with the rentalId provided </exception> 
        void GetRouteDescription(int rentalId, out DateTime startDate, out DateTime endDate, out decimal price, out String originStationId, out String destinationStationId);

        /// <summary>
        /// Gets the descriptions of the user's routes  made within the date interval provided. 
        /// The route description is a string with the following fields splitted by commas: startDate, endDate, price, Origin stationId, Destination stationID
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <exception cref="ServiceException"> Thrown when the interval marked between the start and end date is wrong 
        /// or there are not available routes in that period </exception> 
        /// 
        ICollection<String> GetUserRoutes(DateTime startDate, DateTime endDate);

        void Disconnect();

        int LastRentalId();
    }
}