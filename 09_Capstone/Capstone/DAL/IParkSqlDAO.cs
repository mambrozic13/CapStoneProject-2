using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IParkSqlDAO
    {
        /// <summary>
        /// Gets a list of all parks
        /// </summary>
        /// <returns>IList of parks</returns>
        IList<Park> GetAllParks();

        /// <summary>
        /// Gets information fo a park the user selects
        /// </summary>
        /// <returns></returns>
        void DisplayInfoForPark(Park park);
    }
}
