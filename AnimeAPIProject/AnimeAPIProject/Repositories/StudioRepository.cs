using AnimeAPIProject.Interfaces.Repositories;
using AnimeAPIProject.Models;
using Microsoft.EntityFrameworkCore;
namespace AnimeAPIProject.Repositories
{
    public class StudioRepository : IStudioRepository
    {
        private readonly AnimeContext _context;
        public StudioRepository(AnimeContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Studio>> GetAllAsync()
        {
            return await _context.Studios.ToListAsync();
        }
        public async Task<Studio?> GetByIdAsync(int id)
        {
            return await _context.Studios.FindAsync(id);
        }
        public async Task AddAsync(Studio studio)
        {
            await _context.Studios.AddAsync(studio);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Studio studio)
        {
            _context.Studios.Update(studio);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var studio = await _context.Studios.FindAsync(id);
            if (studio != null)
            {
                _context.Studios.Remove(studio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
