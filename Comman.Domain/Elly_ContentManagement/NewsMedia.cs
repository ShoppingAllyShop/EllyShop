using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_ContentManagement
{
    public partial class NewsMedia
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        /// <summary>
        /// Content
        /// </summary>
        public string? NewsMediaContent { get; set; }
        public string? Url { get; set; }
        public string? Image { get; set; }
    }
}
