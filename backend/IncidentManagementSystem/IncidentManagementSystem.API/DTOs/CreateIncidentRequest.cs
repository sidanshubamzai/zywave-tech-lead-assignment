using IncidentManagementSystem.API.Enums;

namespace IncidentManagementSystem.API.DTOs
{
    public class CreateIncidentRequest
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Severity Severity { get; set; }
    }
}
