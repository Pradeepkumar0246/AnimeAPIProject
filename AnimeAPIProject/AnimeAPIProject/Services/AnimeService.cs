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
            return await _context.Animes
                .Include(a => a.Genres)
                .Include(a => a.Studio)
                .ToListAsync();
        }

        public async Task<Anime?> GetByIdAsync(int id)
        {
            return await _context.Animes
                .Include(a => a.Genres)
                .Include(a => a.Studio)
                .FirstOrDefaultAsync(a => a.Anime_Id == id);
        }

        public async Task<Anime> CreateAsync(Anime anime)
        {
            // Attach genres using IDs
            if (anime.GenreIds != null && anime.GenreIds.Any())
            {
                anime.Genres = new List<Genre>();
                foreach (var genreId in anime.GenreIds)
                {
                    var existingGenre = await _context.Genres.FindAsync(genreId);
                    if (existingGenre != null)
                    {
                        anime.Genres.Add(existingGenre);
                    }
                }
            }

            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
            return anime;
        }

        public async Task<Anime?> UpdateAsync(int id, Anime anime)
        {
            var existingAnime = await _context.Animes
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(a => a.Anime_Id == id);

            if (existingAnime == null) return null;

            // Update properties
            existingAnime.Anime_Name = anime.Anime_Name;
            existingAnime.Anime_Description = anime.Anime_Description;
            existingAnime.Anime_Release_Date = anime.Anime_Release_Date;
            existingAnime.Anime_Episodes = anime.Anime_Episodes;
            existingAnime.Anime_Seasons = anime.Anime_Seasons;
            existingAnime.Studio_Id = anime.Studio_Id;

            // Update genres if GenreIds are provided
            if (anime.GenreIds != null)
            {
                existingAnime.Genres.Clear();
                foreach (var genreId in anime.GenreIds)
                {
                    var existingGenre = await _context.Genres.FindAsync(genreId);
                    if (existingGenre != null)
                    {
                        existingAnime.Genres.Add(existingGenre);
                    }
                }
            }

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

        public async Task<bool> AddGenresToAnimeAsync(int animeId, List<int> genreIds)
        {
            var anime = await _context.Animes
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(a => a.Anime_Id == animeId);

            if (anime == null) return false;

            foreach (var genreId in genreIds)
            {
                var genre = await _context.Genres.FindAsync(genreId);
                if (genre != null && !anime.Genres.Any(g => g.Genre_Id == genreId))
                {
                    anime.Genres.Add(genre);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveGenresFromAnimeAsync(int animeId, List<int> genreIds)
        {
            var anime = await _context.Animes
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(a => a.Anime_Id == animeId);

            if (anime == null) return false;

            foreach (var genreId in genreIds)
            {
                var genre = anime.Genres.FirstOrDefault(g => g.Genre_Id == genreId);
                if (genre != null)
                {
                    anime.Genres.Remove(genre);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
