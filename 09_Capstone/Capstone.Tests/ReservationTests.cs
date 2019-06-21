using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationTests : DAOTests
    {
        [TestMethod]
        public void GetAllReservations_ReturnsCorrectNumberOfResrevations()
        {
            ReservationSqlDAO dao = new ReservationSqlDAO(connectionString);
            
            IList<Reservation> reservationList = dao.GetAllReservationInformation();

            
            Assert.AreEqual(2, reservationList.Count);
        }

        //[TestMethod]
        //public void GetReservationForCampground_ReturnsCorrectNumberOfReservations()
        //{
        //    Park testPark = new Park();
        //    ReservationSqlDAO dao = new ReservationSqlDAO(connectionString);
        //    CampgroundSqlDAO campdao = new CampgroundSqlDAO(connectionString);

        //    //dao.GetReservationsForCampground();
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand($"SELECT * FROM park WHERE name = 'Test'", conn);

        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                testPark.Park_ID = Convert.ToInt32(reader["park_id"]);
        //                testPark.Name = Convert.ToString(reader["name"]);
        //                testPark.Location = Convert.ToString(reader["location"]);
        //                testPark.Establish_date = Convert.ToDateTime(reader["establish_date"]);
        //                testPark.Area = Convert.ToInt32(reader["area"]);
        //                testPark.Visitors = Convert.ToInt32(reader["visitors"]);
        //                testPark.Description = Convert.ToString(reader["description"]);

        //            }

        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine($"There was an error {ex.Message}.");
        //    }
        //    IList<Campground> campgroundList = campdao.GetCampgroundsForPark(testPark);

        //    Campground campground = campgroundList.ElementAt(0);

        //   IList<Reservation> reservationList = dao.GetReservationsForCampground(campground);

        //    Assert.AreEqual(1, reservationList.Count);
        //}

    }
}
