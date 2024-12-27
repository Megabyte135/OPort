using Microsoft.AspNetCore.Mvc;
using Services.Resumes.DTOs;
using Services.Resumes;
using API.Services.Resumes.DTOs;

namespace OPort.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResumesController : ControllerBase
    {
        private readonly ResumesService _resumesService;

        public ResumesController(ResumesService resumesService)
        {
            _resumesService = resumesService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ResumeCreateDto dto)
        {
            await _resumesService.Add(dto);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _resumesService.Delete(id);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ResumeDto dto)
        {
            await _resumesService.Update(dto);
            return Ok();
        }

        [HttpGet("fetch-by-user/{id:guid}")]
        public async Task<ActionResult<List<ResumeDto>>> FetchByUserId([FromRoute] string id)
        {
            var resumes = await _resumesService.FetchByUserId(id);
            return Ok(resumes);
        }

        [HttpGet("fetch")]
        public async Task<ActionResult<List<ResumeDto>>> Fetch([FromQuery] ResumeRequestDto dto)
        {
            var resumes = await _resumesService.Fetch(dto);
            return Ok(resumes);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ResumeDto>> Get([FromRoute] Guid id)
        {
            var resume = await _resumesService.Get(id);
            return Ok(resume);
        }
    }
}
