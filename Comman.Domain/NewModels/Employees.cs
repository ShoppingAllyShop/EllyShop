using System;
using System.Collections.Generic;

namespace Comman.Domain.NewModels
{
    public partial class Employees
    {
        /// <summary>
        /// Uniqueidentifier employee id.
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// Link to user table.
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// Link to position table.
        /// </summary>
        public Guid PositionId { get; set; }
        /// <summary>
        /// Link to department table.
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// The full name.
        /// </summary>
        public string FullName { get; set; } = null!;
        /// <summary>
        /// Gender - 0: Nam, 1: Nữ
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// The date of birth
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Phone number.
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        /// Status: Full-time, Resigned, Probation
        /// </summary>
        public string Status { get; set; } = null!;
        /// <summary>
        /// The started date.
        /// </summary>
        public DateTime StartedDate { get; set; }
        /// <summary>
        /// Salary.
        /// </summary>
        public decimal Salary { get; set; }
        /// <summary>
        /// The time the record was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The last time of the record was updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual Position Position { get; set; } = null!;
        public virtual Users? User { get; set; }
    }
}
