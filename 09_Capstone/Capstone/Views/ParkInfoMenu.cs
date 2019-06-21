using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone.Views
{
    public class ParkInfoMenu : CLIMenu
    {
        Park park = new Park();
        private ICampgroundSqlDAO campgroundDAO;

        public ParkInfoMenu(IParkSqlDAO parkSql, Park parkChoice)
        {
            park = parkChoice;
            parkDAO = parkSql;
            DisplayBeforeMenu(park);

                this.Title = "***Park Infromation Screen***";
                this.menuOptions.Add("1", "View Campgrounds");
                this.menuOptions.Add("2", "Search For Reservation");
                this.menuOptions.Add("Q", "Return to Previous Screen");
            

        }


        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1":
                    // display campgrounds method
                    ICampgroundSqlDAO campgroundDAO = new CampgroundSqlDAO(ConnectionString);
                    ParkCampgroundsMenu menu = new ParkCampgroundsMenu(campgroundDAO, park);
                    menu.Run();
                    break;
                case "2":
                    // Call Reservation Menu
                    campgroundDAO = new CampgroundSqlDAO(ConnectionString);
                    CampgroundReservationMenu menu2 = new CampgroundReservationMenu(campgroundDAO, park);
                    menu2.Run();
                    break;
                case "Q":
                    break;

            }

            return true;
        }

    }
}
