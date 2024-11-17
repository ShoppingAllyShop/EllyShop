using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class GeneralInfomation
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Url { get; set; }
    }
}
