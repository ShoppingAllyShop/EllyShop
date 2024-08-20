using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Image
    {
        public Guid Id { get; set; }
        public string? Type { get; set; }
        public string? Picture { get; set; }
        public string ProductId { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}
