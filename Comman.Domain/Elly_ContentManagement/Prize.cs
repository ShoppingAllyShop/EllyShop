using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_ContentManagement
{
    public partial class Prize
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
        public string? NewContent { get; set; }
        public string? Url { get; set; }
        public string? Image { get; set; }
    }
}
