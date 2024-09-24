using System;
using System.Collections.Generic;

namespace Comman.Domain.Models
{
    public partial class User
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
            UserRoles = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string? PasswordHash { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? CreatedTime { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
