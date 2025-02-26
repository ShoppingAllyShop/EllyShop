using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_ContentManagement
{
    public partial class Branch
    {
        public Guid Id { get; set; }
        public string? CityName { get; set; }
        public string? Address { get; set; }
        public int? CityCode { get; set; }
        public string? BranchName { get; set; }
        /// <summary>
        /// Phân chia theo vùng
        /// </summary>
        public string? Region { get; set; }
    }
}
