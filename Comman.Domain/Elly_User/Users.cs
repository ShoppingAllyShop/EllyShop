using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_User
{
    public partial class Users
    {
        public Users()
        {
            RefreshTokens = new HashSet<RefreshTokens>();
        }

        public Guid Id { get; set; }
        public string UserName { get; set; } = null!;
        public string? PasswordHash { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? CreatedTime { get; set; }
        public Guid RoleId { get; set; }

        public virtual Roles Role { get; set; } = null!;
        public virtual ICollection<RefreshTokens> RefreshTokens { get; set; }
    }
}
