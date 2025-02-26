using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_ContentManagement
{
    public partial class Navigation
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public int? NavLevel { get; set; }
        public Guid? ParentId { get; set; }
        public string? NavContent { get; set; }
        public int? NavIndex { get; set; }
    }
}
