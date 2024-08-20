using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Product
    {
        public Product()
        {
            Images = new HashSet<Image>();
            ProductDetails = new HashSet<ProductDetail>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string? ShortDescription { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? CollectionId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Collection? Collection { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
