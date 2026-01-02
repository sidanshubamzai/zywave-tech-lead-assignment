using IncidentManagementSystem.API.Enums;

namespace IncidentManagementSystem.API.DTOs
{
    public class IncidentResponse
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Severity Severity { get; set; }

        public IncidentStatus Status { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public List<IncidentAttachmentResponse> Attachments { get; set; }
    }
}
