using System.Collections.Concurrent;
using IncidentManagementSystem.API.Models;

namespace IncidentManagementSystem.API.Data
{
    public class InMemoryIncidentRepository : IIncidentRepository
    {
        private readonly ConcurrentDictionary<Guid, Incident> _incidents = new();

        public IEnumerable<Incident> GetAll()
        {
            return _incidents.Values;
        }

        public Incident? GetById(Guid id)
        {
            _incidents.TryGetValue(id, out var incident);
            return incident;
        }

        public Incident Add(Incident incident)
        {
            _incidents[incident.Id] = incident;
            return incident;
        }

        public void Update(Incident incident)
        {
            _incidents[incident.Id] = incident;
        }
    }
}
