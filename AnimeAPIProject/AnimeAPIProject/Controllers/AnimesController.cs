using AnimeAPIProject.Interfaces.Services;
using AnimeAPIProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimesController : ControllerBase
    {
        private readonly IAnimeService _animeService;
        private readonly AnimeContext _context;

        // Single constructor with both dependencies
        public AnimesController(IAnimeService animeService, AnimeContext context)
        {
            _animeService = animeService ?? throw new ArgumentNullException(nameof(animeService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/Animes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anime>>> GetAnimes()
        {
            var animes = await _animeService.GetAllAsync();
            return Ok(animes);
        }

        // GET: api/Animes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Anime>> GetAnime(int id)
        {
            var anime = await _animeService.GetByIdAsync(id);
            if (anime == null)
                return NotFound();

            return Ok(anime);
        }

        // POST: api/Animes
        [HttpPost]
        public async Task<ActionResult<Anime>> PostAnime([FromBody] Anime anime)
        {
            var created = await _animeService.CreateAsync(anime);
            return CreatedAtAction(nameof(GetAnime), new { id = created.Anime_Id }, created);
        }

        // PUT: api/Animes/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Anime>> PutAnime(int id, [FromBody] Anime anime)
        {
            if (id != anime.Anime_Id)
                return BadRequest("Route id and body id must match.");

            var updated = await _animeService.UpdateAsync(id, anime);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/Animes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnime(int id)
        {
            var ok = await _animeService.DeleteAsync(id);
            if (!ok)
                return NotFound();

            return NoContent();
        }

        // GET: api/Animes/genre/5
        [HttpGet("genre/{genreId}")]
        public async Task<ActionResult<IEnumerable<Anime>>> GetAnimesByGenre(int genreId)
        {
            var animes = await _context.Animes
                .Include(a => a.Genres) // load related genres
                .Where(a => a.Genres.Any(g => g.Genre_Id == genreId))
                .ToListAsync();

            if (!animes.Any())
                return NotFound();

            return Ok(animes);
        }

        // GET: api/Animes/studio/5
        [HttpGet("studio/{studioId}")]
        public async Task<ActionResult<IEnumerable<Anime>>> GetAnimesByStudio(int studioId)
        {
            var animes = await _context.Animes
                .Include(a => a.Studio) // load related studio
                .Where(a => a.Studio_Id == studioId)
                .ToListAsync();

            if (!animes.Any())
                return NotFound();

            return Ok(animes);
        }
    }
}
