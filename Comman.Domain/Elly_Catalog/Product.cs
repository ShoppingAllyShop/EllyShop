using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_Catalog
{
    public partial class Product
    {
        public Product()
        {
            ProductDetail = new HashSet<ProductDetail>();
            ProductImages = new HashSet<ProductImages>();
            ProductTags = new HashSet<ProductTags>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? DetailDecription { get; set; }
        public decimal Price { get; set; }
        public decimal? PercentDiscount { get; set; }
        public decimal? Discount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? CollectionId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual Collection? Collection { get; set; }
        public virtual ICollection<ProductDetail> ProductDetail { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }
        public virtual ICollection<ProductTags> ProductTags { get; set; }
    }
}
