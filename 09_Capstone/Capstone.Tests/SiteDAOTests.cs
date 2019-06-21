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
    public class SiteDAOTests : DAOTests
    {
        [DataTestMethod]
        [DataRow("Test", 1, 1)]
        [DataRow("Test2", 1, 1)]
        public void GetAllSitesForCampground_ReturnsCorrectNumberOfSites(string parkName, int campgroundSelection, int expectedCampgrounds)
        {
            ICampgroundSqlDAO campdao = new CampgroundSqlDAO(connectionString);
            ISiteSqlDAO sitedao = new SiteSqlDAO(connectionString);

            Park testPark = new Park();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
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
            IList<Campground> campgroundList = campdao.GetCampgroundsForPark(testPark);

            Campground campground = campgroundList.ElementAt(campgroundSelection - 1);

            IList<Site> siteList = sitedao.GetAllSitesForCampground(campground);

            Assert.AreEqual(expectedCampgrounds, siteList.Count);

        }


        [DataTestMethod]
        [DataRow("Test", 1, 1)]
        public void GetTop5Sites_ReturnsSites(string parkName, int campgroundSelection, int expected)
        {
            ICampgroundSqlDAO campdao = new CampgroundSqlDAO(connectionString);
            ISiteSqlDAO sitedao = new SiteSqlDAO(connectionString);

            Park testPark = new Park();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
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
            IList<Campground> campgroundList = campdao.GetCampgroundsForPark(testPark);

            Campground campground = campgroundList.ElementAt(campgroundSelection - 1);

            IList<Site> siteList = sitedao.GetTop5SitesInCampground(campground, "06/19/2019", "06/20/2019");

            Assert.AreEqual(expected, siteList.Count);
        }

    }
}