using IncidentManagementSystem.API.Models;

public interface IIncidentNotificationService
{
    Task NotifyIncidentCreatedAsync(Incident incident);
}
