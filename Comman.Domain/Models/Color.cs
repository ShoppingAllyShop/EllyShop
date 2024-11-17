using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Color
    {
        public Color()
        {
            ProductDetail = new HashSet<ProductDetail>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ColorCode { get; set; }

        public virtual ICollection<ProductDetail> ProductDetail { get; set; }
    }
}
