
using AnimeAPIProject.Interfaces.Repositories;
using AnimeAPIProject.Interfaces.Services;
using AnimeAPIProject.Models;

namespace AnimeAPIProject.Services
{
    public class StudioService : IStudioService
    {
        private readonly IStudioRepository _repo;
        public StudioService(IStudioRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Studio>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }
        public Task<Studio?> GetByIdAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }
        public async Task<Studio> CreateAsync(Studio studio)
        {
            await _repo.AddAsync(studio);
            return studio;
        }
        public async Task<Studio?> UpdateAsync(int id, Studio studio)
        {
            var existingStudio = await _repo.GetByIdAsync(id);
            if (existingStudio == null) return null;
            existingStudio.Studio_Name = studio.Studio_Name;
            existingStudio.Studio_Description = studio.Studio_Description;
            existingStudio.Studio_Year = studio.Studio_Year;
            existingStudio.Animes = studio.Animes ?? new List<Anime>();
            await _repo.UpdateAsync(existingStudio);
            return existingStudio;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var existingStudio = await _repo.GetByIdAsync(id);
            if (existingStudio == null) return false;
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
