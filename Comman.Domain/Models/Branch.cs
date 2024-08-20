using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Branch
    {
        public Guid Id { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
    }
}
