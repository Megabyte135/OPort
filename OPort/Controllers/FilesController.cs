using Microsoft.AspNetCore.Mvc;
using Services.Resumes.DTOs;
using Services.Resumes;
using Services.Specialities.DTOs;
using Services.Specialities;
using Data;
using Services.Projects;

namespace OPort.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly ProjectsService _projectsService;
        private readonly AppDbContext _context;

        public FilesController(ProjectsService projectsService, AppDbContext context)
        {
            _projectsService = projectsService;
            _context = context;
        }

        [HttpPost("upload/{projectId}")]
        public async Task<IActionResult> UploadFile(Guid projectId, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("‘айл не выбран или пустой.");
            }

            try
            {
                var fileId = await _projectsService.UploadFileAsync(projectId, file);
                return Ok(new { FileId = fileId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetFile(Guid fileId)
        {
            var fileEntity = await _context.Files.FindAsync(fileId);
            if (fileEntity == null)
            {
                return NotFound($"‘айл с Id={fileId} не найден в базе данных.");
            }

            if (!System.IO.File.Exists(fileEntity.FilePath))
            {
                return NotFound("‘айл не найден на диске.");
            }

            var contentType = "application/octet-stream";

            return PhysicalFile(fileEntity.FilePath, contentType, fileEntity.FileName);
        }
    }
}
