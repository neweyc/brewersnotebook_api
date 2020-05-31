using System;
using System.Collections.Generic;
using System.Text;

namespace Beer.Core.Entities
{
    public class Fermentable : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Srm { get; set; }
        public double Ppg { get; set; }
        public double UnitCost { get; set; }
        public bool IsExtract { get; set; }
    }
}
