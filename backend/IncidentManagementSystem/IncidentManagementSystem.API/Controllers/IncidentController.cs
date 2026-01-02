using IncidentManagementSystem.API.Data;
using IncidentManagementSystem.API.DTOs;
using IncidentManagementSystem.API.Models;
using IncidentManagementSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace IncidentManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/incidents")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _incidentService;
        private readonly IIncidentRepository _incidentRepository;

        public IncidentController(IIncidentService incidentService, IIncidentRepository incidentRepository)
        {
            _incidentService = incidentService;
            _incidentRepository = incidentRepository;
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
                    UpdatedAtUtc = i.UpdatedAtUtc,
                    Attachments = i.Attachments.Select(a => new IncidentAttachmentResponse
                    {
                        FileName = a.FileName,
                        BlobUrl = a.BlobUrl,
                        UploadedAtUtc = a.UploadedAtUtc
                    }).ToList()
                });

            return Ok(incidents);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var incident = _incidentService.GetById(id);

            if (incident == null)
                return NotFound();

            var response = new IncidentResponse
            {
                Id = incident.Id,
                Title = incident.Title,
                Description = incident.Description,
                Severity = incident.Severity,
                Status = incident.Status,
                CreatedAtUtc = incident.CreatedAtUtc,
                UpdatedAtUtc = incident.UpdatedAtUtc,
                Attachments = incident.Attachments.Select(a => new IncidentAttachmentResponse
                {
                    FileName = a.FileName,
                    BlobUrl = a.BlobUrl,
                    UploadedAtUtc = a.UploadedAtUtc
                }).ToList()
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult>  Create([FromBody] CreateIncidentRequest request)
        {
            var incident = await _incidentService.CreateAsync(
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
       // [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAttachment(Guid id, IFormFile file, [FromServices] IBlobStorageService blobStorageService)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is required");
            if (file.Length > 5 * 1024 * 1024)
                return BadRequest("Max file size is 5 MB");

            var incident = _incidentRepository.GetById(id);
            if (incident == null)
                return NotFound();

            var fileName = $"{id}/{Guid.NewGuid()}_{file.FileName}";
            var blobUrl = await blobStorageService.UploadAsync(file, fileName);

            incident.Attachments.Add(new IncidentAttachment
            {
                FileName = file.FileName,
                BlobUrl = blobUrl,
                UploadedAtUtc = DateTime.UtcNow
            });

            return Ok(incident.Attachments);
        }

    }
}
