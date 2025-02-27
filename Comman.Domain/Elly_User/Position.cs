using System;
using System.Collections.Generic;

namespace Comman.Domain.Elly_User
{
    public partial class Position
    {
        /// <summary>
        /// Uniqueidentifier position id.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Name of position.
        /// </summary>
        public string PositionName { get; set; } = null!;
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
