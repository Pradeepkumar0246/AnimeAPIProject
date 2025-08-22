using AnimeAPIProject.Interfaces.Repositories;
using AnimeAPIProject.Models;
using Microsoft.EntityFrameworkCore;
namespace AnimeAPIProject.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AnimeContext _context;
        public GenreRepository(AnimeContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }
        public async Task AddAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
}
