
using AnimeAPIProject.Models;

namespace AnimeAPIProject.Interfaces.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<Genre?> GetByIdAsync(int id);
        Task<Genre> CreateAsync(Genre genre);
        Task<Genre?> UpdateAsync(int id, Genre genre);
        Task<bool> DeleteAsync(int id);
    }
}
