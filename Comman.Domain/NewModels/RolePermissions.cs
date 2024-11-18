using System;
using System.Collections.Generic;

namespace Comman.Domain.NewModels
{
    public partial class RolePermissions
    {
        public Guid Id { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? PermissionId { get; set; }

        public virtual Permissions? Permission { get; set; }
        public virtual Roles? Role { get; set; }
    }
}
