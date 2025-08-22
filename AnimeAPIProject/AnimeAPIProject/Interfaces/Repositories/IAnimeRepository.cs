using AnimeAPIProject.Models;
namespace AnimeAPIProject.Interfaces.Repositories
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAllAsync();
        Task<Anime?> GetByIdAsync(int id);
        Task AddAsync(Anime anime);
        Task UpdateAsync(Anime anime);
        Task DeleteAsync(int id);
        Task<IEnumerable<Anime>> GetByGenreAsync(int genreId);
    }
}
