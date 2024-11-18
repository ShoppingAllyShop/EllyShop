using System;
using System.Collections.Generic;

namespace Comman.Domain.NewModels
{
    public partial class RefreshTokens
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime Expiry { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Users? User { get; set; }
    }
}
