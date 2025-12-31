using IncidentManagementSystem.API.Models;

namespace IncidentManagementSystem.API.Data
{
    public interface IIncidentRepository
    {
        IEnumerable<Incident> GetAll();
        Incident? GetById(Guid id);
        Incident Add(Incident incident);
        void Update(Incident incident);
    }
}
