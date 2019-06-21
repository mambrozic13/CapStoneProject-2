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
        /// Runs a method to get all the reservations for a campground
        /// </summary>
        /// <param name="">Campground</param>
        /// <returns>IList</returns>
        IList<Reservation> GetReservationsForCampground(Campground campground);
    }
}
