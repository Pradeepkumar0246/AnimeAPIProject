using AnimeAPIProject.Models;

namespace AnimeAPIProject.Interfaces.Repositories
{
    public interface IStudioRepository
    {
        Task<IEnumerable<Studio>> GetAllAsync();
        Task<Studio?> GetByIdAsync(int id);
        Task AddAsync(Studio studio);
        Task UpdateAsync(Studio studio);
        Task DeleteAsync(int id);
    }
}
