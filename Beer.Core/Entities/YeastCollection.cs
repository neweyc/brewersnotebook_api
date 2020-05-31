using System;
using System.Collections.Generic;
using System.Text;

namespace Beer.Core.Entities
{
    public class YeastCollection : Entity
    {
        public string UserEmail { get; set; }
        public List<Yeast> Yeast { get; set; }
    }
}
