using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Name { get; set; } = null!;
        public int CategoryLevel { get; set; }
        /// <summary>
        /// 0:Woman,1:Men
        /// </summary>
        public bool? Gender { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
