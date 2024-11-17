using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Tags
    {
        public Tags()
        {
            ProductTags = new HashSet<ProductTags>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Type { get; set; }
        public string? TypeName { get; set; }

        public virtual ICollection<ProductTags> ProductTags { get; set; }
    }
}
