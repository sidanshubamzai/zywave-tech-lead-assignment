using IncidentManagementSystem.API.Enums;

namespace IncidentManagementSystem.API.Models
{
    public class Incident
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Severity Severity { get; set; }

        public IncidentStatus Status { get; set; } = IncidentStatus.Open;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAtUtc { get; set; }

        public List<IncidentAttachment> Attachments { get; set; } = new();
    }
}
