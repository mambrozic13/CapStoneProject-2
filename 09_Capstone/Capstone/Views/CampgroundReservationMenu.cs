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
        string arrivalDate;
        string departureDate;
        //DateTime arrivalDate = new DateTime();
        //DateTime departureDate = new DateTime();
        ISiteSqlDAO siteDAO = new SiteSqlDAO(ConnectionString);
        Campground campground = new Campground();
        Park park = new Park();
        public CampgroundReservationMenu(ICampgroundSqlDAO campgroundSql, IList<Campground> campgroundListOrig)
        {

            IList<Campground> campgroundList = campgroundListOrig;

            DisplayInfoForCampground(campgroundList);
            //DisplayBeforeMenu(park);

            this.Title = "***Search for Campground Reservation***";
            DisplayInformation();
        }

        public bool DisplayInformation()
        {
            int selectedCampground = GetInteger("Which Campground (Enter 0 To Cancel) ");
            campground = campgroundList.ElementAt(selectedCampground - 1);
                        
            if (selectedCampground < 1)
            {
                return false;
            }
            if (selectedCampground > campgroundList.Count)
            {
                Console.WriteLine("That is not a valid park.");
                Pause("");
                return false;
            }
            

            arrivalDate = GetString("What is the expected arrival date? (mm/dd/yyyy)");
            if(!DateTime.TryParse(arrivalDate, out DateTime result))
            {
                Console.WriteLine("That is not a valid date.");
                Pause("");
                return false;
            }
            departureDate = GetString("What is the expected departure date? (mm/dd/yyyy) ");
            if(!DateTime.TryParse(departureDate, out DateTime departResult))
            {
                Console.WriteLine("That is not a valid date.");
                Pause("");
                return false;
            }
            DisplayAvaliableSiteHeader();
            DisplayInfoForReservation();
            Console.ReadKey();
            return true;
        }
       
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1":
                    break;
                case "2":
                    break;
                case "Q":
                    break;

            }
            return true;
        }

        public void DisplayAvaliableSiteHeader()
        {
            Console.WriteLine("Results Matching Your Search Criteria.");
            Console.WriteLine($"Site No");
            Console.Write($"\t Max Occup.");
            Console.Write($"\t Accessible?");
            Console.Write($"\t Max RV Length");
            Console.Write($"\t Utility?");
            Console.Write($"\t Cost");
        }

        public void DisplayInfoForReservation()
        {

            IList<Site> topFiveSiteList = siteDAO.GetTop5SitesInCampground(campground, arrivalDate, departureDate);
            foreach(Site campsite in topFiveSiteList)
            {
                Console.WriteLine("");
                Console.Write($"\t {campsite.Site_Number}");
                Console.Write($"\t {campsite.Max_Occupancy}");
                Console.Write($"\t {campsite.Accessible}");
                Console.Write($"\t {campsite.Max_RV_Length}");
                Console.Write($"\t {campsite.Utilities}");
                Console.WriteLine($"\t {campground.Daily_Fee}");
            }
        }

        public void DisplayInfoForCampground(IList<Campground> campgroundList)
        {

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
