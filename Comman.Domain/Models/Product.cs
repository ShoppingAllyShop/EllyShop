using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Product
    {
        public Product()
        {
            Image = new HashSet<Image>();
            ProductDetail = new HashSet<ProductDetail>();
            ProductTags = new HashSet<ProductTags>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? DetailDecription { get; set; }
        public decimal Price { get; set; }
        public decimal? PercentDiscount { get; set; }
        public decimal? Discount { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? CollectionId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Collection? Collection { get; set; }
        public virtual ICollection<Image> Image { get; set; }
        public virtual ICollection<ProductDetail> ProductDetail { get; set; }
        public virtual ICollection<ProductTags> ProductTags { get; set; }
    }
}
