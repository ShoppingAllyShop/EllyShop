using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Silde
    {
        public Guid Id { get; set; }
        public string? Picture { get; set; }
        public string? Position { get; set; }
    }
}
