using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Views
{
    public class CLIMainMenu : CLIMenu
    {
        IList<Park> parkList = new List<Park>();
        public CLIMainMenu(IParkSqlDAO parksql)
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
                case "1":
                    // do some work
                    // Passes info into the next section from list
                    Park park1 = parkList.ElementAt(0);
                    parkDAO.DisplayInfoForPark(park1);
                    break;
                case "2":
                    // do some work
                    // Passes info into the next section from list
                    Park park2 = parkList.ElementAt(1);
                    parkDAO.DisplayInfoForPark(park2);
                    break;
                case "3":
                    // do some work
                    // Passes info into the next section from list
                    Park park3 = parkList.ElementAt(2);
                    parkDAO.DisplayInfoForPark(park3);
                    break;
                case "Q":
                    break;
            }

            return true;
        }
    }
}
