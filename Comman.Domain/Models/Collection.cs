using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Collection
    {
        public Collection()
        {
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
