using System;
using System.Collections.Generic;
using System.Text;

namespace Beer.Core.Entities
{
    public class Recipe : Entity
    {
        public enum RecipeType { AllGrain=1, PartialMash=2}
        public string Name { get; set; }
        public double BrewhouseEfficiency { get; set; }
        public double BoilVolume { get; set; }
        public double TotalVolume { get; set; }
        public double Og { get; set; }
        public double Ibus { get; set; }
        public double Srm { get; set; }
        public Yeast Yeast { get; set; }
        public List<FermentableAddition> Fermentables { get; set; }
        public List<HopAddition> Hops { get; set; }
        public string Notes { get; set; }
        public RecipeType Type { get; set; }


    }
}
