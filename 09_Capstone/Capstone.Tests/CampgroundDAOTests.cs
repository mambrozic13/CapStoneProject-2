using Capstone.DAL;
using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.Tests
{
    [TestClass]
    public class CampgroundDAOTests : DAOTests
    {
        [DataTestMethod]
        [DataRow("Test", 1)]
        [DataRow("Test2", 1)]
        public void GetCampgroundsForPark_ReturnsCorrectCampgrounds(string parkName, int expectedResult)
        {
            ICampgroundSqlDAO dao = new CampgroundSqlDAO(connectionString);
            Park testPark = new Park();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM park WHERE name = '{parkName}'", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        testPark.Park_ID = Convert.ToInt32(reader["park_id"]);
                        testPark.Name = Convert.ToString(reader["name"]);
                        testPark.Location = Convert.ToString(reader["location"]);
                        testPark.Establish_date = Convert.ToDateTime(reader["establish_date"]);
                        testPark.Area = Convert.ToInt32(reader["area"]);
                        testPark.Visitors = Convert.ToInt32(reader["visitors"]);
                        testPark.Description = Convert.ToString(reader["description"]);

                    }

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"There was an error {ex.Message}.");
            }
            IList<Campground> campgroundList = dao.GetCampgroundsForPark(testPark);

            Assert.AreEqual(1, campgroundList.Count);
        }


    }
}
