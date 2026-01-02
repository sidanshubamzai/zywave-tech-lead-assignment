using System;

namespace IncidentNotificationFunction.Models
{
    public class IncidentNotificationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Severity { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
