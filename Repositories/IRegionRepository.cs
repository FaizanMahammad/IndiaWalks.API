using IndiaWalks.API.Models.Domain;

namespace IndiaWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id,Region region);
        Task<Region?> DeleteAsync(Guid id);
    }
}
