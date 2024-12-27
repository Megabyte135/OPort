using Microsoft.AspNetCore.Mvc;
using Services.Projects;
using Services.Projects.DTOs;

namespace OPort.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsService _projectsService;

        public ProjectsController(ProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProject(
            [FromForm] ProjectCreateDto dto,
            IFormFile projectCover) 
        {
            try
            {
                var createdProjectDto = await _projectsService.CreateProjectWithFileAsync(dto, projectCover);
                return Ok(createdProjectDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDto>> GetAsync([FromRoute] Guid id)
        {
            var project = await _projectsService.GetAsync(id);
            return Ok(project);
        }

        [HttpGet("fetch-by-resume/{resumeId:guid}")]
        public async Task<ActionResult<List<ProjectDto>>> FetchByResumeIdAsync([FromRoute] Guid resumeId)
        {
            var projects = await _projectsService.FetchByResumeIdAsync(resumeId);
            return Ok(projects);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProjectDto>> UpdateAsync([FromRoute] Guid id, [FromBody] ProjectCreateDto dto)
        {
            var updatedProject = await _projectsService.UpdateAsync(id, dto);
            return Ok(updatedProject);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _projectsService.DeleteAsync(id);
            return Ok();
        }
    }
}
