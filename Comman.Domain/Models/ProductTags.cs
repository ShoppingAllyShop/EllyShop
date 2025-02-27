using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class ProductTags
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; } = null!;
        public Guid TagId { get; set; }
        public DateTime? AssignedDate { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Tags Tag { get; set; } = null!;
    }
}
