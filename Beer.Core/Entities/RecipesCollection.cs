using System.Collections.Generic;

namespace Beer.Core.Entities
{
    public class RecipesCollection : Entity
    {
        public string UserEmail { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
