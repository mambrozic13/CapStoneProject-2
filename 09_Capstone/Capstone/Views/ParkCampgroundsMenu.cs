using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Views
{
    public class ParkCampgroundsMenu : CLIMenu
    {
        Park park = new Park();
        private ICampgroundSqlDAO campgroundDAO;

        public ParkCampgroundsMenu(ICampgroundSqlDAO campgroundSql, Park parkChoice)
        {
            campgroundDAO = campgroundSql;
            park = parkChoice;

            campgroundList = campgroundDAO.GetCampgroundsForPark(park);

            DisplayInfoForCampground(campgroundList);
            Console.ReadKey();
            this.Title = "***Park Campgrounds***";
            this.Title = $"{parkChoice.Name} National Park";

            this.menuOptions.Add("1", "Search For Available Reservation");
            this.menuOptions.Add("Q", "Return to Previous Screen");




        }

        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1":
                    // Call and execute reservation menu
                    CampgroundReservationMenu resMenu = new CampgroundReservationMenu(campgroundDAO, campgroundList);
                    resMenu.Run();
                    break;

                case "Q":
                    break;

            }

            return true;
        }

        public void DisplayInfoForCampground(IList<Campground> campgroundList)
        {
            Console.Clear();
            Console.WriteLine($"{park.Name} Park Campgrounds");
            Console.WriteLine($"");
            Console.Write($"\t Name");
            Console.Write($"\t Open");
            Console.Write($"\t Close");
            Console.Write($"\t Daily Fee");
            Console.WriteLine("");

            foreach (Campground campground in campgroundList)
            {
                Console.Write($"\t {campground.Name}");
                Console.Write($"\t {campground.Open_From_MM}");
                Console.Write($"\t {campground.Open_To_MM}");
                Console.Write($"\t {campground.Daily_Fee}");
                Console.WriteLine();
            }


            Console.ReadKey();
        }

    }
}
