using System.Collections.Generic;

namespace Beer.Core.Entities
{
    public class BatchCollection : Entity
    {
        public string UserEmail { get; set; }
        public List<Batch> Batches { get; set; }
    }
}
