using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeAPIProject.Models;

namespace AnimeAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly AnimeContext _context;

        public GenreController(AnimeContext context)
        {
            _context = context;
        }

        // GET: api/Genre
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        // GET: api/Genre/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return NotFound();
            return genre;
        }

        // GET: api/Genre/5/anime-count
        [HttpGet("{id}/anime-count")]
        public async Task<IActionResult> GetAnimeCountByGenre(int id)
        {
            var result = await _context.Genres
                .Where(g => g.Genre_Id == id)
                .Select(g => new { GenreId = g.Genre_Id, AnimeCount = g.Animes.Count })
                .FirstOrDefaultAsync();

            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET: api/Genre/5/animes
        [HttpGet("{id}/animes")]
        public async Task<IActionResult> GetAnimesByGenre(int id)
        {
            var animes = await _context.Animes
                .Include(a => a.Studio)
                .Include(a => a.Genres)
                .Where(a => a.Genres.Any(g => g.Genre_Id == id))
                .ToListAsync();

            if (!animes.Any()) return NotFound("No anime found for this genre.");
            return Ok(animes);
        }

        // GET: api/Genre/with-counts
        [HttpGet("with-counts")]
        public async Task<IActionResult> GetAllGenresWithCounts()
        {
            var data = await _context.Genres
                .Select(g => new
                {
                    g.Genre_Id,
                    g.Genre_Name,
                    AnimeCount = g.Animes.Count
                })
                .ToListAsync();

            return Ok(data);
        }

        // POST: api/Genre
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGenre), new { id = genre.Genre_Id }, genre);
        }

        // PUT: api/Genre/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            if (id != genre.Genre_Id) return BadRequest();
            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Genre/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return NotFound();

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool GenreExists(int id) => _context.Genres.Any(e => e.Genre_Id == id);
    }
}
