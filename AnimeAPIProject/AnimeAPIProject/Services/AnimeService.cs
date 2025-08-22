
using AnimeAPIProject.Interfaces.Repositories;
using AnimeAPIProject.Interfaces.Services;
using AnimeAPIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeAPIProject.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly AnimeContext _context;

        public AnimeService(AnimeContext context)
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
        public async Task<Anime> CreateAsync(Anime anime)
        {
            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
            return anime;
        }
        public async Task<Anime?> UpdateAsync(int id, Anime anime)
        {
            var existingAnime = await _context.Animes.FindAsync(id);
            if (existingAnime == null) return null;
            existingAnime.Anime_Name = anime.Anime_Name;
            existingAnime.Anime_Description = anime.Anime_Description;
            existingAnime.Anime_Release_Date = anime.Anime_Release_Date;
            existingAnime.Anime_Episodes = anime.Anime_Episodes;
            existingAnime.Anime_Seasons = anime.Anime_Seasons;
            existingAnime.Studio_Id= anime.Studio_Id;
            existingAnime.Genres = anime.Genres ?? new List<Genre>();
            _context.Animes.Update(existingAnime);
            await _context.SaveChangesAsync();
            return existingAnime;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var existingAnime = await _context.Animes.FindAsync(id);
            if (existingAnime == null) return false;
            _context.Animes.Remove(existingAnime);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
