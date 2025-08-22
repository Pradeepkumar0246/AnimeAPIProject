
using AnimeAPIProject.Models;

namespace AnimeAPIProject.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users?> GetByIdAsync(int id);
        Task<Users> CreateAsync(Users users);
        Task<Users?> UpdateAsync(int id, Users users);
        Task<bool> DeleteAsync(int id);
    }
}
