using System;
using System.Collections.Generic;

namespace Comman.Domain.NewModels
{
    public partial class Permissions
    {
        public Permissions()
        {
            RolePermissions = new HashSet<RolePermissions>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<RolePermissions> RolePermissions { get; set; }
    }
}
