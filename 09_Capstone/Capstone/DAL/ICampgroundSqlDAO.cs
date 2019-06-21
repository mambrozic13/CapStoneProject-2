﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface ICampgroundSqlDAO
    {
       // IList<Campground> GetAllCampgrounds();

        IList<Campground> GetCampgroundsForPark(Park park);
    }
}
