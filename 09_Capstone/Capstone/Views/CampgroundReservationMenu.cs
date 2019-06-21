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

        //DateTime arrivalDate = new DateTime();
        //DateTime departureDate = new DateTime();
        ISiteSqlDAO siteDAO = new SiteSqlDAO(ConnectionString);
        Campground campground = new Campground();
        IList<Campground> campgroundList;
        Park park;
        
        public CampgroundReservationMenu(ICampgroundSqlDAO campgroundSql, Park park)
        {
            this.Title = "***Search for Campground Reservation***";
            this.park = park;
            campgroundList = campgroundSql.GetCampgroundsForPark(park);

            DisplayInfoForCampground(campgroundList);
            //DisplayBeforeMenu(park);



            DisplayInformation();
        }

        public bool DisplayInformation()
        {
            int selectedCampground = GetInteger("Which Campground (Enter 0 To Cancel) ");
            Campground campground = campgroundList.ElementAt(selectedCampground - 1);
                        
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
            

            string arrivalDate = GetString("What is the expected arrival date? (mm/dd/yyyy)");
            if(!DateTime.TryParse(arrivalDate, out DateTime result))
            {
                Console.WriteLine("That is not a valid date.");
                Pause("");
                return false;
            }
            string departureDate = GetString("What is the expected departure date? (mm/dd/yyyy) ");
            if(!DateTime.TryParse(departureDate, out DateTime departResult))
            {
                Console.WriteLine("That is not a valid date.");
                Pause("");
                return false;
            }
            DisplayAvaliableSiteHeader();
            DisplayInfoForReservation(campground, arrivalDate, departureDate);
            Console.ReadKey();
            return true;
        }
       
        protected override bool ExecuteSelection(string choice)
        {
           
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

        private void DisplayInfoForReservation(Campground campground, string arrivalDate, string departureDate)
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
            int siteNumber = GetInteger("Which site should be reserved (enter 0 to cancel)?");
            string name = GetString("What name should the reservation be made under?");

            Site site = topFiveSiteList.ElementAt(siteNumber - 1);

            ReservationSqlDAO ReservationDAO = new ReservationSqlDAO(ConnectionString);
            int reservationId = ReservationDAO.CreateNewReservation(site, name, arrivalDate, departureDate);
            Console.WriteLine($"The reservation has been made and the confirmation id is {reservationId}");
            Pause("");
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
