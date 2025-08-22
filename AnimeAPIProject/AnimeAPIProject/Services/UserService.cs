
using AnimeAPIProject.Interfaces.Repositories;
using AnimeAPIProject.Interfaces.Services;
using AnimeAPIProject.Models;

namespace AnimeAPIProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        public Task<IEnumerable<Users>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Users?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public async Task<Users> CreateAsync(Users users)
        {
            await _repo.AddAsync(users);
            return users;
        }
        public async Task<Users?> UpdateAsync(int id, Users users)
        {
            var existingUser = await _repo.GetByIdAsync(id);
            if (existingUser == null) return null;
            existingUser.User_Name = users.User_Name;
            existingUser.User_Email = users.User_Email;
            existingUser.User_Password = users.User_Password;
            existingUser.Role = users.Role;
            existingUser.WatchedAnimes = users.WatchedAnimes ?? new List<Anime>();
            await _repo.UpdateAsync(existingUser);
            return existingUser;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var existingUser = await _repo.GetByIdAsync(id);
            if (existingUser == null) return false;
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
