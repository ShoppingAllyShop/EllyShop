using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class NewsMedia
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string? NewsMediaContent { get; set; }
        public string? Url { get; set; }
        public string? Image { get; set; }
    }
}
