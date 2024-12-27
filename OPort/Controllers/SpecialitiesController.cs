using Microsoft.AspNetCore.Mvc;
using Services.Resumes.DTOs;
using Services.Resumes;
using Services.Specialities.DTOs;
using Services.Specialities;

namespace OPort.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialitiesController : ControllerBase
    {
        private readonly SpecialitiesService _specialitiesService;

        public SpecialitiesController(SpecialitiesService specialitiesService)
        {
            _specialitiesService = specialitiesService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] SpecialityCreateDto dto)
        {
            await _specialitiesService.AddAsync(dto);
            return Ok();
        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _specialitiesService.Delete(id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpecialityDto?>> Get([FromRoute] int id)
        {
            var speciality = await _specialitiesService.Get(id);
            if (speciality == null)
                return NotFound();
            return Ok(speciality);
        }

        [HttpGet("fetch")]
        public async Task<ActionResult<List<SpecialityDto>>> Fetch()
        {
            var specialities = await _specialitiesService.Fetch();
            return Ok(specialities);
        }
    }
}
