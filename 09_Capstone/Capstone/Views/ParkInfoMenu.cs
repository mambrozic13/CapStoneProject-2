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

        IList<Campground> campgroundList = new List<Campground>();
        public ParkInfoMenu(ICampgroundSqlDAO campgroundSql, IParkSqlDAO parkSql, Park park)
        {
            campgroundDAO = campgroundSql;
            parkDAO = parkSql;

          DisplayBeforeMenu(park, menu);

                this.Title = "***Park Infromation Screen***";
                this.menuOptions.Add("1", "View Campgrounds");
                this.menuOptions.Add("2", "Search For Reservation");
                this.menuOptions.Add("3", "Return to Previous Screen");
            

        }

        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1":
                    // do some work
                    // Passes info into the next section from list
                   // Park park1 = parkList.ElementAt(0);
                   // parkDAO.DisplayInfoForPark(park1);
                    break;
                case "2":
                    // do some work
                    // Passes info into the next section from list
                    // park2 = parkList.ElementAt(1);
                   // parkDAO.DisplayInfoForPark(park2);
                    break;
                case "3":

                    break;

            }

            return true;
        }

    }
}
