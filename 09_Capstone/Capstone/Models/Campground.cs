using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Campground
    {
        public int Campground_ID { get; set; }
        public int Park_ID { get; set; }
        public string Name { get; set; }
        public DateTime Open_From_MM { get; set; }
        public DateTime Open_To_MM { get; set; }
        public decimal Daily_Fee { get; set; }

        public override string ToString()
        {
            return Campground_ID.ToString() + Park_ID.ToString() + Name + Open_From_MM.ToString() + Open_To_MM.ToString() + Daily_Fee.ToString();
        }
    }
}
