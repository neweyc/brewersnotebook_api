using System;
using System.Collections.Generic;
using System.Text;

namespace Beer.Core.Entities
{
    public class FermentableCollection : Entity
    {
        public string UserEmail { get; set; }
        public List<Fermentable> Fermentables { get; set; }
    }
}
