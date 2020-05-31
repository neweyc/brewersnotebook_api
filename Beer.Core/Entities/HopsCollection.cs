using System;
using System.Collections.Generic;
using System.Text;

namespace Beer.Core.Entities
{
    public class HopsCollection : Entity
    {
        public string UserEmail { get; set; }
        public List<Hop> Hops { get; set; }
    }
}
