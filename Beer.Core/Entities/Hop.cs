using System;
using System.Collections.Generic;
using System.Text;

namespace Beer.Core.Entities
{

    public class Hop : Entity
    {
        public enum HopType { Pellet=1, Whole=2}
        public string Name { get; set; }      
        public HopType Type { get; set; }
        public double AlphaAcidPercentage { get; set; }
        public double BetaAcidPercentage { get; set; }
        public string Description { get; set; }
        public double UnitCost { get; set; }
       
    }
}
