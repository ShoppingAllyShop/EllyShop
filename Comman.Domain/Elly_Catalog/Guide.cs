using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_Catalog
{
    public partial class Guide
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? GuideContent { get; set; }
    }
}
