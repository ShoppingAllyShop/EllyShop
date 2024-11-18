using System;
using System.Collections.Generic;

namespace Comman.Domain.NewModels
{
    public partial class Roles
    {
        public Roles()
        {
            RolePermissions = new HashSet<RolePermissions>();
            Users = new HashSet<Users>();
        }

        public Guid Id { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<RolePermissions> RolePermissions { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
