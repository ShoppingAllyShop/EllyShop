using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_Catalog
{
    public partial class Color
    {
        public Color()
        {
            ProductDetail = new HashSet<ProductDetail>();
            ProductImages = new HashSet<ProductImages>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ColorCode { get; set; }

        public virtual ICollection<ProductDetail> ProductDetail { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }
    }
}
