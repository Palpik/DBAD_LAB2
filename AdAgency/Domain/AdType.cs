using System;
using System.Collections.Generic;

namespace Domain
{
    public partial class AdType
    {
        public AdType()
        {
            AdPlaces = new HashSet<AdPlace>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<AdPlace> AdPlaces { get; set; }
    }
}
