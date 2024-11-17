using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Rating
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? ProductId { get; set; }
        public int? Point { get; set; }
    }
}
