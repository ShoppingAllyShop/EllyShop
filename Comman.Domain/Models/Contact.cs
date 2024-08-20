using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
