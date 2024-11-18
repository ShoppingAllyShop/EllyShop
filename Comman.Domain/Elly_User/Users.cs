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
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Phone { get; set; }
        public Guid RoleId { get; set; }

        public virtual Roles Role { get; set; } = null!;
        public virtual Employees? Employees { get; set; }
        public virtual ICollection<RefreshTokens> RefreshTokens { get; set; }
    }
}
