using AnimeAPIProject.Interfaces.Repositories;
using AnimeAPIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeAPIProject.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AnimeContext _context;
        public AnimeRepository(AnimeContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Anime>> GetAllAsync()
        {
            return await _context.Animes.ToListAsync();
        }
        public async Task<Anime?> GetByIdAsync(int id)
        {
            return await _context.Animes.FindAsync(id);
        }
        public async Task AddAsync(Anime anime)
        {
            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Anime anime)
        {
            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var anime = await _context.Animes.FindAsync(id);
            if (anime != null)
            {
                _context.Animes.Remove(anime);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Anime>> GetByGenreAsync(int genreId)
        {
            return await _context.Animes
                .Where(a => a.Genres.Any(g => g.Genre_Id == genreId))
                .ToListAsync();
        }
    }
}
