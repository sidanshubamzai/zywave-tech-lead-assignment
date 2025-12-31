using IncidentManagementSystem.API.Data;
using IncidentManagementSystem.API.Enums;
using IncidentManagementSystem.API.Models;

namespace IncidentManagementSystem.API.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IIncidentRepository _repository;
        private readonly IIncidentNotificationService _notificationService;

        public IncidentService(IIncidentRepository repository,
        IIncidentNotificationService notificationService)
        {
            _repository = repository;
            _notificationService = notificationService;
        }

        public IEnumerable<Incident> GetAll()
        {
            return _repository.GetAll();
        }

        public Incident GetById(Guid id)
        {
            var incident = _repository.GetById(id);
            if (incident == null)
            {
                throw new KeyNotFoundException($"Incident with id {id} not found");
            }

            return incident;
        }

        public async Task<Incident> CreateAsync(string title, string description, Severity severity)
        {
            var incident = new Incident
            {
                Title = title,
                Description = description,
                Severity = severity,
                Status = IncidentStatus.Open
            };

            _repository.Add(incident);

            await _notificationService.NotifyIncidentCreatedAsync(incident);

            return incident;

        }

        public Incident UpdateStatus(Guid id, IncidentStatus status)
        {
            var incident = GetById(id);

            incident.Status = status;
            incident.UpdatedAtUtc = DateTime.UtcNow;

            _repository.Update(incident);
            return incident;
        }
    }
}
