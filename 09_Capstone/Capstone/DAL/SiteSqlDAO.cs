using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteSqlDAO
    {
        private string connectionString;

        // Create a new sql-based reservation dao.
        public SiteSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }
        // Create a list to hold all the site information for a campground.
        public static IList<Site> siteListAll = new List<Site>();

        // Create a list to hold all the available site information
        public static IList<Site> siteListTopFive = new List<Site>();

        // Create a list to hold all the reserved site information
        public static IList<Site> siteListReserved = new List<Site>();

        public IList<Site> GetAllSitesForCampground(Campground campground)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM site WHERE site.campground_ID = @campgroundID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@campgroundID", campground.Campground_ID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = new Site();
                        site.Site_ID = Convert.ToInt32(reader["site_id"]);
                        site.Campground_ID = Convert.ToInt32(reader["campground_id"]);
                        site.Site_Number = Convert.ToInt32(reader["site_number"]);
                        site.Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        site.Accessible = Convert.ToBoolean(reader["accessible"]);
                        site.Max_RV_Length = Convert.ToInt32(reader["max_rv_length"]);
                        site.Utilities = Convert.ToBoolean(reader["utilities"]);
                        siteListAll.Add(site);
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"There was an error: {e.Message}.");
                Console.ReadKey();
            }
            return siteListAll;
        }

        public IList<Site> GetTop5SitesInCampground(Campground campground, string fromDate, string toDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT TOP 5 * FROM site WHERE site.campground_id = @campgroundID AND site.site_id NOT IN (SELECT reservation.site_id FROM reservation WHERE from_date BETWEEN @from_date AND @to_date OR to_date BETWEEN @from_date AND @to_date);";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@campgroundID", campground.Campground_ID);
                    cmd.Parameters.AddWithValue("@from_date", fromDate);
                    cmd.Parameters.AddWithValue("@to_date", toDate);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = new Site();
                        site.Site_ID = Convert.ToInt32(reader["site_id"]);
                        site.Campground_ID = Convert.ToInt32(reader["campground_id"]);
                        site.Site_Number = Convert.ToInt32(reader["site_number"]);
                        site.Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        site.Accessible = Convert.ToBoolean(reader["accessible"]);
                        site.Max_RV_Length = Convert.ToInt32(reader["max_rv_length"]);
                        site.Utilities = Convert.ToBoolean(reader["utilities"]);
                        siteListTopFive.Add(site);
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"There was an error: {e.Message}.");
                Console.ReadKey();
            }

            return siteListTopFive;
        }

        public IList<Site> GetReservedSitesInCampground(Campground campground, DateTime arrivalDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM reservation AS r INNER JOIN site AS s ON r.site_id = s.site_id INNER JOIN campground AS c ON s.campground_id = c.campground_id WHERE c.name = @name AND exists (SELECT * FROM reservation WHERE @arrivalDate NOT BETWEEN(select min(from_date) from reservation) AND(select max(to_date) from reservation))";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", campground.Name);
                    cmd.Parameters.AddWithValue("@arrivalDate", arrivalDate);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Site site = new Site();
                        site.Site_ID = Convert.ToInt32(reader["site_id"]);
                        site.Campground_ID = Convert.ToInt32(reader["campground_id"]);
                        site.Site_Number = Convert.ToInt32(reader["site_number"]);
                        site.Max_Occupancy = Convert.ToInt32(reader["max_occupancy"]);
                        site.Accessible = Convert.ToBoolean(reader["accessible"]);
                        site.Max_RV_Length = Convert.ToInt32(reader["max_rv_length"]);
                        site.Utilities = Convert.ToBoolean(reader["utilities"]);
                        siteListReserved.Add(site);
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"There was an error: {e.Message}.");
                Console.ReadKey();
            }

            return siteListReserved;
        }

    
    }
}
