using IncidentManagementSystem.API.DTOs;
using IncidentManagementSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IncidentManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/incidents")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var incidents = _incidentService.GetAll()
                .Select(i => new IncidentResponse
                {
                    Id = i.Id,
                    Title = i.Title,
                    Description = i.Description,
                    Severity = i.Severity,
                    Status = i.Status,
                    CreatedAtUtc = i.CreatedAtUtc,
                    UpdatedAtUtc = i.UpdatedAtUtc
                });

            return Ok(incidents);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var incident = _incidentService.GetById(id);

            var response = new IncidentResponse
            {
                Id = incident.Id,
                Title = incident.Title,
                Description = incident.Description,
                Severity = incident.Severity,
                Status = incident.Status,
                CreatedAtUtc = incident.CreatedAtUtc,
                UpdatedAtUtc = incident.UpdatedAtUtc
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateIncidentRequest request)
        {
            var incident = _incidentService.Create(
                request.Title,
                request.Description,
                request.Severity);

            var response = new IncidentResponse
            {
                Id = incident.Id,
                Title = incident.Title,
                Description = incident.Description,
                Severity = incident.Severity,
                Status = incident.Status,
                CreatedAtUtc = incident.CreatedAtUtc,
                UpdatedAtUtc = incident.UpdatedAtUtc
            };

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPatch("{id:guid}/status")]
        public IActionResult UpdateStatus(Guid id, [FromBody] UpdateIncidentStatusRequest request)
        {
            var incident = _incidentService.UpdateStatus(id, request.Status);

            var response = new IncidentResponse
            {
                Id = incident.Id,
                Title = incident.Title,
                Description = incident.Description,
                Severity = incident.Severity,
                Status = incident.Status,
                CreatedAtUtc = incident.CreatedAtUtc,
                UpdatedAtUtc = incident.UpdatedAtUtc
            };

            return Ok(response);
        }

        [HttpPost("{id}/attachments")]
        public async Task<IActionResult> UploadAttachment(Guid id, IFormFile file, [FromServices] IBlobStorageService blobStorageService)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required");

            var fileName = $"{id}/{Guid.NewGuid()}_{file.FileName}";
            var blobUrl = await blobStorageService.UploadAsync(file, fileName);

            return Ok(new
            {
                FileName = file.FileName,
                BlobUrl = blobUrl
            });
        }

    }
}
