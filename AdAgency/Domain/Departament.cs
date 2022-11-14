using System;
using System.Collections.Generic;

namespace Domain
{
    public partial class Departament
    {
        public Departament()
        {
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; set; }
    }
}
