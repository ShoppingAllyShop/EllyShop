using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string? NewsContent { get; set; }
    }
}
