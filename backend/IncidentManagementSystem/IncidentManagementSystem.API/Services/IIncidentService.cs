using IncidentManagementSystem.API.Enums;
using IncidentManagementSystem.API.Models;

namespace IncidentManagementSystem.API.Services
{
    public interface IIncidentService
    {
        IEnumerable<Incident> GetAll();
        Incident GetById(Guid id);
        Task<Incident> CreateAsync(
         string title,
         string description,
         Severity severity);
        Incident UpdateStatus(Guid id, IncidentStatus status);
    }
}
