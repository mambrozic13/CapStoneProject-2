using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ParkSQLDAO : IParkSqlDAO
    {
        private string connectionString;

        // Create a new sql-based park dao.
        public ParkSQLDAO(string databaseconnectionString)
        {
            connectionString = databaseconnectionString;
        }

        public static IList<Park> parkList = new List<Park>();

        public IList<Park> GetAllParks()
        {

            // Create a list to hold the parks
           

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Park park = new Park();
                        park.Park_ID = Convert.ToInt32(reader["park_id"]);
                        park.Name = Convert.ToString(reader["name"]);
                        park.Location = Convert.ToString(reader["location"]);
                        park.Establish_date = Convert.ToDateTime(reader["establish_date"]);
                        park.Area = Convert.ToInt32(reader["area"]);
                        park.Visitors = Convert.ToInt32(reader["visitors"]);
                        park.Description = Convert.ToString(reader["description"]);

                        parkList.Add(park);
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"There was an error: {e.Message}.");
            }
            return parkList;
        }

        //public void DisplayInfoForPark(Park park)
        //{
        //    Console.Clear();
        //    Console.WriteLine("Park Information Screen");
        //    Console.WriteLine($"{park.Name} National Park");
        //    Console.WriteLine($"Location: {park.Location}");
        //    Console.WriteLine($"Established: {park.Establish_date}");
        //    Console.WriteLine($"Area: {park.Area}");
        //    Console.WriteLine($"Annual Visitors: {park.Visitors}");
        //    Console.WriteLine("");
        //    Console.WriteLine($"{park.Description}");

        //    Console.ReadKey();
        //}
    }
    
}
