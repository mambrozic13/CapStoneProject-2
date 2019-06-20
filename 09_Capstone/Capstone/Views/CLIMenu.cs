using Capstone.DAL;
using Capstone.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.Views
{
    public abstract class CLIMenu
    {
        protected IParkSqlDAO parkDAO;
        protected ICampgroundSqlDAO campgroundDAO;
        public static string ConnectionString { get; set; }

        public static IList<Park> parkList = new List<Park>();
        public static IList<Campground> campgroundList = new List<Campground>();
        public static IList<Site> siteList = new List<Site>();
        public static IList<Reservation> reservationList = new List<Reservation>();

        /*** 
         * Model Data that this menu system needs to operate on goes here.
         ***/
        protected Park NationalParks { get; set; }
        /// <summary>
        /// This is where every sub-menu puts its options for display to the user.
        /// </summary>
        protected Dictionary<string, string> menuOptions;

        /// <summary>
        /// The Title of this menu
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Constructor - pass in model data here
        /// </summary>
        public CLIMenu()
        {
            this.menuOptions = new Dictionary<string, string>();
        }

        /// <summary>
        /// Run starts the menu loop
        /// </summary>
        public void Run()
        {
            while (true)
            {
                //Console.Clear();
                DisplayBeforeMenu();

                Console.WriteLine(this.Title);
                Console.WriteLine(new string('=', this.Title.Length));
                Console.WriteLine("\r\nPlease make a selection:");
                foreach (KeyValuePair<string, string> menuItem in menuOptions)
                {
                    Console.WriteLine($"{menuItem.Key} - {menuItem.Value}");
                }

                DisplayAfterMenu();

                string choice = GetString("Selection:").ToUpper();

                if (menuOptions.ContainsKey(choice))
                {
                    if (choice == "Q")
                    {
                        break;
                    }
                    if (!ExecuteSelection(choice))
                    {
                        break;
                    }
                }

            }
        }

        /// <summary>
        /// Given a valid menu selection, runs the approriate code to do what the user is asking for.
        /// </summary>
        /// <param name="choice">The menu option (key) selected by the user</param>
        /// <returns>True to keep executing the menu (loop), False to exit this menu (break)</returns>
        abstract protected bool ExecuteSelection(string choice);

        /// <summary>
        /// DisplayBeforeMenu is a virtaul mathod called after the screen is cleared and before the 
        /// menu options are displayed to the user.
        /// 
        /// Override this if you want to display your own information before the menu choices.
        /// </summary>
        virtual protected void DisplayBeforeMenu()
        {
            return;
        }

        virtual protected void DisplayBeforeMenu(Park park)
        {
            //DisplayInfoForPark(park);
            return;
        }

        /// <summary>
        /// DisplayAfterMenu is a virtaul mathod called after the menu options are displayed
        /// and before the user is prompted for a selection.
        /// 
        /// Override this if you want to display your own information after the menu choices.
        /// It should be no mare than 1 or 2 lines.
        /// 
        /// </summary>
        virtual protected void DisplayAfterMenu()
        {
            return;
        }



        public void GetAllParks()
        {

            //    // Create a list to hold the parks
            //    IList<Park> parkList = new List<Park>();

            //    try
            //    {
            //        using (SqlConnection conn = new SqlConnection(connectionString))
            //        {
            //            conn.Open();
            //            SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);

            //            SqlDataReader reader = cmd.ExecuteReader();

            //            while (reader.Read())
            //            {
            //                Park park = new Park();
            //                park.Park_ID = Convert.ToInt32(reader["park_id"]);
            //                park.Name = Convert.ToString(reader["name"]);
            //                park.Location = Convert.ToString(reader["location"]);
            //                park.Establish_date = Convert.ToDateTime(reader["establish_date"]);
            //                park.Area = Convert.ToInt32(reader["area"]);
            //                park.Visitors = Convert.ToInt32(reader["visitors"]);
            //                park.Description = Convert.ToString(reader["description"]);

            //                parkList.Add(park);
            //            }
            //        }
            //    }
            //    catch (SqlException e)
            //    {
            //        Console.WriteLine($"There was an error: {e.Message}.");
            //    }
            //}

            //// Create a new sql-based park dao.
            //public CLIMenu(string databaseconnectionString)
            //{
            //    connectionString = databaseconnectionString;
            //}
        }
            #region User Input Helper Methods
            /// <summary>
            /// This continually prompts the user until they enter a valid integer.
            /// </summary>
            /// <param name="message"></param>
            /// <returns></returns>
            protected int GetInteger(string message)
            {
                int resultValue = 0;
                while (true)
                {
                    Console.Write(message + " ");
                    string userInput = Console.ReadLine().Trim();
                    if (int.TryParse(userInput, out resultValue))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("!!! Invalid input. Please enter a valid whole number.");
                    }
                }
                return resultValue;
            }

            /// <summary>
            /// This continually prompts the user until they enter a valid double.
            /// </summary>
            /// <param name="message"></param>
            /// <returns></returns>
            protected double GetDouble(string message)
            {
                double resultValue = 0;
                while (true)
                {
                    Console.Write(message + " ");
                    string userInput = Console.ReadLine().Trim();
                    if (double.TryParse(userInput, out resultValue))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("!!! Invalid input. Please enter a valid decimal number.");
                    }
                }
                return resultValue;
            }

            /// <summary>
            /// This continually prompts the user until they enter a valid bool.
            /// </summary>
            /// <param name="message"></param>
            /// <returns></returns>
            protected bool GetBool(string message)
            {
                bool resultValue = false;
                while (true)
                {
                    Console.Write(message + " ");
                    string userInput = Console.ReadLine().Trim();
                    if (userInput.ToUpper() == "Y")
                    {
                        resultValue = true;
                        break;
                    }
                    else if (userInput == "N")
                    {
                        resultValue = false;
                        break;
                    }
                    else if (bool.TryParse(userInput, out resultValue))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("!!! Invalid input. Please enter [True, False, Y or N].");
                    }
                }
                return resultValue;
            }

            /// <summary>
            /// This continually prompts the user until they enter a valid string (1 or more characters).
            /// </summary>
            /// <param name="message"></param>
            /// <returns></returns>
            protected string GetString(string message)
            {
                while (true)
                {
                    Console.Write(message + " ");
                    string userInput = Console.ReadLine().Trim();
                    if (!String.IsNullOrEmpty(userInput))
                    {
                        return userInput;
                    }
                    else
                    {
                        Console.WriteLine("!!! Invalid input. Please enter a valid decimal number.");
                    }
                }
            }

            /// <summary>
            /// Shows a message to the user and waits for the user to hit return
            /// </summary>
            /// <param name="message"></param>
            protected void Pause(string message)
            {
                Console.Write(message + " Press Enter to continue.");
                Console.ReadLine();
            }
            #endregion



        
    }
}

