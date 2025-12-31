using IncidentManagementSystem.API.Models;
using IncidentManagementSystem.API.Enums;

namespace IncidentManagementSystem.API.Services
{
    public interface IIncidentService
    {
        IEnumerable<Incident> GetAll();
        Incident GetById(Guid id);
        Incident Create(string title, string description, Severity severity);
        Incident UpdateStatus(Guid id, IncidentStatus status);
    }
}
