using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Views
{
    public class MainMenu : CLIMenu
    {
        IList<Park> parkList = new List<Park>();
        public MainMenu(IParkSqlDAO parksql)
        {
            parkDAO = parksql;
            try
            { 
                this.Title = "***Select A Park For Further Details***";
                parkList = parkDAO.GetAllParks();
                foreach(Park park in parkList)
                {
                    this.menuOptions.Add($"{park.Park_ID}", $"{park.Name}");
                }
                this.menuOptions.Add("Q", "Quit");
            }
            catch(Exception e)
            {
                Console.WriteLine($"There was an error: {e.Message}.");
                Console.ReadKey();
            }
        }
        protected override bool ExecuteSelection(string choice)
        {
            
            switch (choice)
            {
                case "Q":
                    break;

                 default:
                    int index = int.Parse(choice) - 1;
                    Park park = parkList.ElementAt(index);
                    DisplayInfoForPark(park);
                  break;
            }

            return true;
        }
        public void DisplayInfoForPark(Park park)
        {
            Console.Clear();
            Console.WriteLine("Park Information Screen");
            Console.WriteLine($"{park.Name} National Park");
            Console.WriteLine($"Location: {park.Location}");
            Console.WriteLine($"Established: {park.Establish_date}");
            Console.WriteLine($"Area: {park.Area}");
            Console.WriteLine($"Annual Visitors: {park.Visitors}");
            Console.WriteLine("");
            Console.WriteLine($"{park.Description}");

            Console.ReadKey();
        }
    }
}
