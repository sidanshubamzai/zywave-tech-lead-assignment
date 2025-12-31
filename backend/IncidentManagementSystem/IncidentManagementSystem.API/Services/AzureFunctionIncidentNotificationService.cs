using IncidentManagementSystem.API.Models;
using System.Text;
using System.Text.Json;

public class AzureFunctionIncidentNotificationService
    : IIncidentNotificationService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AzureFunctionIncidentNotificationService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task NotifyIncidentCreatedAsync(Incident incident)
    {
        var functionUrl = _configuration["AzureFunction:IncidentCreatedUrl"];

        var payload = new
        {
            incident.Id,
            incident.Title,
            incident.Description,
            incident.Severity,
            incident.Status,
            incident.CreatedAtUtc
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(functionUrl, content);

        response.EnsureSuccessStatusCode();
    }
}
