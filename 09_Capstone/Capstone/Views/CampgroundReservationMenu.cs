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
            DisplayInformation();
        }

        public bool DisplayInformation()
        {
            int selectedCampground = GetInteger("Which Campground (Enter 0 To Cancel) ");
            if (selectedCampground < 1)
            {
                Console.WriteLine("That is not a valid park.");
                Pause("");
                return false;
            }
            Campground campground = campgroundList.ElementAt(selectedCampground - 1);
                        

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
            return true;
        }
       


        public void DisplayAvaliableSiteHeader()
        {
            Console.WriteLine("Results Matching Your Search Criteria.");
            Console.WriteLine("");
            Console.Write($"Site No".PadRight(10));
            Console.Write($"\t Max Occup.".PadRight(10));
            Console.Write($"\t Accessible?".PadRight(10));
            Console.Write($"\t Max RV Length".PadRight(10));
            Console.Write($"\t Utility?".PadRight(10));
            Console.Write($"\t Cost".PadRight(10));
        }

        private void DisplayInfoForReservation(Campground campground, string arrivalDate, string departureDate)
        {
            int count = 1;
            IList<Site> topFiveSiteList = siteDAO.GetTop5SitesInCampground(campground, arrivalDate, departureDate);
            foreach(Site campsite in topFiveSiteList)
            {
                    string Accessible = ReturnYesORNoForBool(campsite.Accessible);
                    string Utilities = ReturnYesORNoForBool(campsite.Utilities);
                    Console.WriteLine("");
                    Console.Write($"{count}){campsite.Site_Number}".PadRight(10));
                    Console.Write($"\t {campsite.Max_Occupancy}".PadRight(10));
                    Console.Write($"\t {Accessible}".PadRight(10));
                    Console.Write($"\t {campsite.Max_RV_Length}".PadRight(10));
                    Console.Write($"\t {Utilities}".PadRight(10));
                    Console.Write($"\t {campground.Daily_Fee:C}".PadRight(10));
                    Console.WriteLine("");
                count++;
            }
            int siteNumber = GetInteger("Which site should be reserved (enter 0 to cancel)?");
            string name = GetString("What name should the reservation be made under?");

            Site site = topFiveSiteList.ElementAt(siteNumber - 1);

            ReservationSqlDAO ReservationDAO = new ReservationSqlDAO(ConnectionString);
            int reservationId = ReservationDAO.CreateNewReservation(site, name, arrivalDate, departureDate);
            Console.WriteLine($"The reservation has been made and the confirmation id is {reservationId}");
            Pause("");
            ViewParksMenu menu = new ViewParksMenu();
            menu.Run();
        }



        public void DisplayInfoForCampground(IList<Campground> campgroundList)
        {
            
            string campgroundOpenMonth;
            string campgroundCloseMonth;
            Console.Clear();
            Console.WriteLine($"{park.Name} Park Campgrounds");
            Console.WriteLine($"");
            Console.Write(" ".PadRight(6) + "Name".PadRight(35));
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

        protected override bool ExecuteSelection(string choice)
        {
           return true;
        }
    }
}
