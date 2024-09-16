using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime Expiry { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
