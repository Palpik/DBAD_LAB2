using System;
using System.Collections.Generic;

namespace Domain
{
    public partial class Post
    {
        public Post()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int DepId { get; set; }

        public virtual Departament Dep { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
