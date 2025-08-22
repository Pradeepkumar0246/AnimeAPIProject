using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            if (genre == null)
            {
                return NotFound();
            }
            return genre;
        }
        // POST: api/Genre
        [HttpPost]
        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGenre", new { id = genre.Genre_Id }, genre);
        }
        // PUT: api/Genre/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            if (id != genre.Genre_Id)
            {
                return BadRequest();
            }
            _context.Entry(genre).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        // DELETE: api/Genre/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.Genre_Id == id);
        }
    }
}
