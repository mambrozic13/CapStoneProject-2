using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface ISiteSqlDAO
    {
        // This is the command that wil get us the reserved sites
        //("SELECT * FROM reservation INNER JOIN site ON reservation.site_id = site.site_id INNER JOIN campground ON site.campground_id = campground.campground_id WHERE campground.name = @campgroundName AND exists (SELECT * FROM reservation" + WHERE @arrivalDate NOT BETWEEN (select min(from_date) from reservation) AND (select max(to_date) from reservation));", conn);


        // This is the one to get the top 5 available sites
        //@"SELECT TOP 5 * FROM site INNER JOIN campground ON site.campground_id = campground.campground_id WHERE campground.name = 'Blackwoods' AND site_id NOT IN (SELECT site_id FROM reservation WHERE from_date BETWEEN '@from_date' AND '-' OR to_date BETWEEN '' AND '' OR (from_date BETWEEN '' AND '' AND to_date BETWEEN ' AND ' OR from_date< '' AND to_date > '')

        IList<Site> GetTop5SitesInCampground(Campground campground, string fromDate, string toDate);

       IList<Site> GetAllSitesForCampground(Campground campground);

       IList<Site> GetReservedSitesInCampground(Campground campground, DateTime arrivalDate);

    }
}
