using System;
using System.Collections.Generic;
using System.Text;

namespace Beer.Core.Entities
{
    public class Batch : Entity
    {
        public DateTime BrewDate { get; set; }
        public DateTime? SecondaryDate { get; set; }
        public double Og { get; set; }
        public double Fg { get; set; }
        public int Rating { get; set; }
        public string Notes { get; set; }
        public Recipe Recipe { get; set; }
    }
}
