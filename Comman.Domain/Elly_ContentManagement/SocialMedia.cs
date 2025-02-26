using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_ContentManagement
{
    public partial class SocialMedia
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
        public string? Url { get; set; }
    }
}
