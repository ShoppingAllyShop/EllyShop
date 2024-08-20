using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Size
    {
        public Size()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public Guid Id { get; set; }
        public string? Size1 { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
