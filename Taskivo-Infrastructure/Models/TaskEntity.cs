using System;
using System.Collections.Generic;

namespace Taskivo_Infrastructure.Models
{
    public partial class TaskEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public short Status { get; set; }

        public short Priority { get; set; }

        public StatusEntity? StatusDetails { get; set; }

        public PriorityEntity? PriorityDetails { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CompletedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
