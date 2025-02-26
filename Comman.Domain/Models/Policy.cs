using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Policy
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
    }
}
