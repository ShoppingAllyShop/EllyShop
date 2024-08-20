using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class ProductDetail
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public int? Quantity { get; set; }
        public Guid? SizeId { get; set; }
        public Guid? ColorId { get; set; }
        public string? ProductId { get; set; }
        public string? DetailDecription { get; set; }

        public virtual Color? Color { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Size? Size { get; set; }
    }
}
