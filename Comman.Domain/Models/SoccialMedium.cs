using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class SoccialMedium
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
    }
}
