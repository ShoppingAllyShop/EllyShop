using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_User
{
    public partial class Department
    {
        /// <summary>
        /// Uniqueidentifier department id.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name of department
        /// </summary>
        public string DepartmentName { get; set; } = null!;
        /// <summary>
        /// The department is active or not (1: active, 0: inactive).
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// The time the record was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The last time of the record was updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        public virtual Employees? Employees { get; set; }
    }
}
