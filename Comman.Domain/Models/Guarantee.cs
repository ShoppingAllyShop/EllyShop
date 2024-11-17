using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Guarantee
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Contents { get; set; }
        public string? Icon { get; set; }
    }
}
