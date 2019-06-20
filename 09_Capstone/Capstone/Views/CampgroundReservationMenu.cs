using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Views
{

    public class CampgroundReservationMenu : CLIMenu
    {
        string campgroundName;
        DateTime arrivalDate = new DateTime();
        DateTime departureDate = new DateTime();


        Park park = new Park();
        public CampgroundReservationMenu(ICampgroundSqlDAO campgroundSql, IList<Campground> campgroundListOrig)
        {

            IList<Campground> campgroundList = campgroundListOrig;

            DisplayInfoForCampground(campgroundList);
            //DisplayBeforeMenu(park);

            this.Title = "***Search for Campground Reservation***";
            DisplayInformation();
        }

        public void DisplayInformation()
        {
            Console.WriteLine("Which Campground (Enter Q To Cancel): ");            
            campgroundName = Console.ReadLine();
            try
            {
                Console.WriteLine("What is the arrival date?: ");
                arrivalDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("What is the departure date?: ");
                departureDate = Convert.ToDateTime(Console.ReadLine());
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error: {ex.Message}.");
                Console.ReadKey();
            }

        }

        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1":
                    break;
                case "2":
                    // Call Reservation Menu
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
