using AnimeAPIProject.Interfaces.Services;
using AnimeAPIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimeAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudioController : ControllerBase
    {
        private readonly IStudioService _service;
        public StudioController(IStudioService service)
        {
            _service = service;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var studios = await _service.GetAllAsync();
            return Ok(studios);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var studio = await _service.GetByIdAsync(id);
            if (studio == null) return NotFound();
            return Ok(studio);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Studio studio)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdStudio = await _service.CreateAsync(studio);
            return CreatedAtAction(nameof(GetById), new { id = createdStudio.Studio_Id }, createdStudio);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Studio studio)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedStudio = await _service.UpdateAsync(id, studio);
            if (updatedStudio == null) return NotFound();
            return Ok(updatedStudio);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
