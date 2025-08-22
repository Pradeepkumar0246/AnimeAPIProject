
using AnimeAPIProject.Models;

namespace AnimeAPIProject.Interfaces.Services
{
    public interface IStudioService
    {
        Task<IEnumerable<Studio>> GetAllAsync();
        Task<Studio?> GetByIdAsync(int id);
        Task<Studio> CreateAsync(Studio studio);
        Task<Studio?> UpdateAsync(int id, Studio studio);
        Task<bool> DeleteAsync(int id);
    }
}
