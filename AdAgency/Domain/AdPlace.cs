using System;
using System.Collections.Generic;

namespace Domain
{
    public partial class AdPlace
    {
        public AdPlace()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Place { get; set; } = null!;
        public int TypeId { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; } = null!;

        public virtual AdType Type { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
