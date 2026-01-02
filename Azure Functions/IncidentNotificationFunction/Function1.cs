using IncidentNotificationFunction.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;


namespace IncidentNotificationFunction
{
    public class IncidentNotificationFunction
    {
        private readonly ILogger<IncidentNotificationFunction> _logger;

        public IncidentNotificationFunction(ILogger<IncidentNotificationFunction> logger)
        {
            _logger = logger;
        }

        [Function("IncidentCreatedNotification")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var incident = JsonSerializer.Deserialize<IncidentNotificationDto>(
                requestBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (incident == null)
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid incident payload");
                return badResponse;
            }

            _logger.LogInformation(
                "Incident created | Id: {Id} | Title: {Title} | Severity: {Severity} | Status: {Status}",
                incident.Id,
                incident.Title,
                incident.Severity,
                incident.Status);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("Incident notification processed");
            return response;
        }
    }
}
