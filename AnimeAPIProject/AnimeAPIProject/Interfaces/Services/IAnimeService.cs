
using AnimeAPIProject.Models;

namespace AnimeAPIProject.Interfaces.Services
{
    public interface IAnimeService
    {
        Task<IEnumerable<Anime>> GetAllAsync();
        Task<Anime> GetByIdAsync(int id);
        Task<Anime> CreateAsync(Anime anime);
        Task<Anime> UpdateAsync(int id, Anime anime);  // ✅ fix
        Task<bool> DeleteAsync(int id);
    }
}
