using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_ContentManagement
{
    public partial class News
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? NewContent { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Url { get; set; }
        public string? Image { get; set; }
    }
}
