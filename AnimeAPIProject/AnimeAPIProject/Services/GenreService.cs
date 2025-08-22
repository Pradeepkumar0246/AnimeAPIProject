
using AnimeAPIProject.Interfaces.Repositories;
using AnimeAPIProject.Interfaces.Services;
using AnimeAPIProject.Models;

namespace AnimeAPIProject.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repo;
        public GenreService(IGenreRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Genre>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }
        public Task<Genre?> GetByIdAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }
        public async Task<Genre> CreateAsync(Genre genre)
        {
            await _repo.AddAsync(genre);
            return genre;
        }
        public async Task<Genre?> UpdateAsync(int id, Genre genre)
        {
            var existingGenre = await _repo.GetByIdAsync(id);
            if (existingGenre == null) return null;
            existingGenre.Genre_Name = genre.Genre_Name;
            existingGenre.Animes = genre.Animes ?? new List<Anime>();
            await _repo.UpdateAsync(existingGenre);
            return existingGenre;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var existingGenre = await _repo.GetByIdAsync(id);
            if (existingGenre == null) return false;
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
