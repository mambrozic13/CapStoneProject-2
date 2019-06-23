using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IReservationSqlDAO
    {
        /// <summary>
        /// Runs a method to populate the reservation properties
        /// </summary>
        /// <returns>List</returns>
        IList<Reservation> GetAllReservationInformation();

        /// <summary>
        /// Creates a new reservation in the database
        /// </summary>
        /// <param name="site">Site object</param>
        /// <param name="name">Name of the site in db</param>
        /// <param name="arrivalDate">Users arrival date to the park</param>
        /// <param name="departureDate">Users departure date from the park</param>
        /// <returns>Reservation ID</returns>
        int CreateNewReservation(Site site, string name, string arrivalDate, string departureDate);
    }
}
