using AnimeAPIProject.Interfaces.Services;
using AnimeAPIProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudioController : ControllerBase
    {
        private readonly IStudioService _service;
        private readonly AnimeContext _context;

        public StudioController(IStudioService service, AnimeContext context)
        {
            _service = service;
            _context = context;
        }

        // ✅ Get all studios
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var studios = await _service.GetAllAsync();
            return Ok(studios);
        }

        // ✅ Get studio by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var studio = await _service.GetByIdAsync(id);
            if (studio == null) return NotFound();
            return Ok(studio);
        }

        // ✅ New Feature: Get Anime Count by Studio
        [HttpGet("{id}/anime-count")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAnimeCountByStudio(int id)
        {
            var count = await _context.Animes.CountAsync(a => a.Studio_Id == id);
            return Ok(new { StudioId = id, AnimeCount = count });
        }

        // ✅ New Feature: Get Anime List by Studio
        [HttpGet("{id}/animes")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAnimesByStudio(int id)
        {
            var animes = await _context.Animes.Where(a => a.Studio_Id == id).ToListAsync();
            if (!animes.Any()) return NotFound("No anime found for this studio.");
            return Ok(animes);
        }

        // ✅ New Feature: Get Total Studio Count
        [HttpGet("count")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetTotalStudios()
        {
            var count = await _context.Studios.CountAsync();
            return Ok(new { TotalStudios = count });
        }

        // ✅ Create new studio
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Studio studio)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdStudio = await _service.CreateAsync(studio);
            return CreatedAtAction(nameof(GetById), new { id = createdStudio.Studio_Id }, createdStudio);
        }

        // ✅ Update studio
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Studio studio)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedStudio = await _service.UpdateAsync(id, studio);
            if (updatedStudio == null) return NotFound();
            return Ok(updatedStudio);
        }

        // ✅ Delete studio
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
