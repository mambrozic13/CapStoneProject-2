using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Site
    {
        public int Site_ID { get; set; }
        public int Campground_ID { get; set; }
        public int Site_Number { get; set; }
        public int Max_Occupancy { get; set; }
        public bool Accessible { get; set; }
        public int Max_RV_Length { get; set; }
        public bool Utilities { get; set; }

        public override string ToString()
        {
            return Site_ID.ToString() + Campground_ID.ToString() + Site_Number.ToString() + Max_Occupancy.ToString() + Accessible.ToString() + Max_RV_Length.ToString() + Utilities.ToString();
        }
    }
}
