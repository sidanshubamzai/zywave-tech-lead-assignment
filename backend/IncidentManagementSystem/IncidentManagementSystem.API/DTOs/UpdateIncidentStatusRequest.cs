using IncidentManagementSystem.API.Enums;

namespace IncidentManagementSystem.API.DTOs
{
    public class UpdateIncidentStatusRequest
    {
        public IncidentStatus Status { get; set; }
    }
}
