using AnimeAPIProject.Models;

namespace AnimeAPIProject.Interfaces.Services
{
    public interface IAnimeService
    {
        Task<IEnumerable<Anime>> GetAllAsync();
        Task<Anime?> GetByIdAsync(int id);
        Task<Anime> CreateAsync(Anime anime);
        Task<Anime?> UpdateAsync(int id, Anime anime);  // ✅ changed to nullable
        Task<bool> DeleteAsync(int id);

        Task<bool> AddGenresToAnimeAsync(int animeId, List<int> genreIds);
        Task<bool> RemoveGenresFromAnimeAsync(int animeId, List<int> genreIds);
    }
}
