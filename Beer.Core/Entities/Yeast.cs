using System;
using System.Collections.Generic;
using System.Text;

namespace Beer.Core.Entities
{
    public class Yeast : Entity
    {
        public string Name { get; set; }
        public double Attenuation { get; set; }
        public double UnitCost { get; set; }
        public string Description { get; set; }
    }
}
