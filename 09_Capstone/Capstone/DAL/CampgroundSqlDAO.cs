using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO
    {
        private string connectionString;

        // Create a new sql-based park dao.
        public CampgroundSqlDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public IList<Campground> GetCampgroundsForPark(int park_id)
        {
            // Create a list to hold campgrounds
            IList<Campground> campgrounds = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM campground as c JOIN park as p on c.park_id = p.park_id WHERE p.park_id = @park_id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@park_id", park_id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground campground = new Campground();
                        campground.Campground_ID = Convert.ToInt32(reader["campground_id"]);
                        campground.Park_ID = Convert.ToInt32(reader["park_id"]);
                        campground.Name = Convert.ToString(reader["name"]);
                        campground.Open_From_MM = Convert.ToDateTime(reader["open_from_mm"]);
                        campground.Open_To_MM = Convert.ToDateTime(reader["open_to_mm"]);
                        campground.Daily_Fee = Convert.ToDecimal(reader["daily_fee"]);
                        campgrounds.Add(campground);    
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"There was an error: {e.Message}.");
                Console.ReadKey();
            }
            return campgrounds;
        }

        public IList<Campground> GetAllCampgrounds()
        {
            // Create a list to hold campgrounds
            IList<Campground> campgrounds = new List<Campground>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT * FROM campground";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Campground campground = new Campground();
                        campground.Campground_ID = Convert.ToInt32(reader["campground_id"]);
                        campground.Park_ID = Convert.ToInt32(reader["park_id"]);
                        campground.Name = Convert.ToString(reader["name"]);
                        campground.Open_From_MM = Convert.ToDateTime(reader["open_from_mm"]);
                        campground.Open_To_MM = Convert.ToDateTime(reader["open_to_mm"]);
                        campground.Daily_Fee = Convert.ToDecimal(reader["daily_fee"]);
                        campgrounds.Add(campground);
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"There was an error: {e.Message}.");
                Console.ReadKey();
            }
            return campgrounds;
        }

        public void DisplayInfoForCampground(Campground campground)
        {
            Console.Clear();
            Console.WriteLine("Park Campgrounds");
            Console.WriteLine($"");
            Console.Write($"\t {campground.Name}");
            Console.Write($"\t {campground.Open_From_MM}");
            Console.Write($"\t {campground.Open_To_MM}");
            Console.Write($"\t {campground.Daily_Fee}");
            

            Console.ReadKey();
        }
    }
}
