using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_User
{
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public Guid Id { get; set; }
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Users> Users { get; set; }
    }
}
