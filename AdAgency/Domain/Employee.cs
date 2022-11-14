using System;
using System.Collections.Generic;

namespace Domain
{
    public partial class Employee
    {
        public Employee()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public int? PositionId { get; set; }

        public virtual Post? Position { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
