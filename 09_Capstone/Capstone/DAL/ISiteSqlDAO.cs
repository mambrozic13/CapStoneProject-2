using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface ISiteSqlDAO
    {

        IList<Site> GetTop5SitesInCampground(Campground campground, string fromDate, string toDate);

       IList<Site> GetAllSitesForCampground(Campground campground);

       IList<Site> GetReservedSitesInCampground(Campground campground, DateTime arrivalDate);

        

    }
}
