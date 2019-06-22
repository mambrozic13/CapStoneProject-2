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
        Park park;
        private ICampgroundSqlDAO campgroundDAO;
        IList<Campground> campgroundList;

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
                    CampgroundReservationMenu resMenu = new CampgroundReservationMenu(campgroundDAO, park);
                    resMenu.Run();
                    break;

                case "Q":
                    break;

            }

            return true;
        }

        public void DisplayInfoForCampground(IList<Campground> campgroundList)
        {
            string campgroundOpenMonth;
            string campgroundCloseMonth;
            Console.Clear();
            Console.WriteLine($"{park.Name} Park Campgrounds");
            Console.WriteLine($"");
            Console.Write(" " .PadRight(6) +  "Name".PadRight(35));
            Console.Write("Open".PadRight(20));
            Console.Write($"Close".PadRight(20));
            Console.Write($"Daily Fee".PadRight(10));
            Console.WriteLine("");
            int count = 1;
                foreach (Campground campground in campgroundList)
                {

                    campgroundOpenMonth = ReturnMonthForInt(campground.Open_From_MM);
                    campgroundCloseMonth = ReturnMonthForInt(campground.Open_To_MM);

                    Console.Write("".PadRight(4) + $"{count}){ campground.Name}".PadRight(35));

                    Console.Write($"{campgroundOpenMonth}".PadRight(20));
                    Console.Write($"{campgroundCloseMonth}".PadRight(21));
                    Console.Write("".PadRight(3) + $"{campground.Daily_Fee:C}".PadRight(3));
                    Console.WriteLine();
                    count++;
                }
            Console.ReadKey();
        }
    }
}
