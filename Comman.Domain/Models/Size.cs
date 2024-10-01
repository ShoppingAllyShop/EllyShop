using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Size
    {
        public Size()
        {
            ProductDetail = new HashSet<ProductDetail>();
        }

        public Guid Id { get; set; }
        public string? Size1 { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<ProductDetail> ProductDetail { get; set; }
    }
}
