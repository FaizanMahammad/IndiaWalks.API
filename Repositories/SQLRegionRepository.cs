using IndiaWalks.API.Data;
using IndiaWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IndiaWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly IndiaWalksDbContext dbContext;
        public SQLRegionRepository(IndiaWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);  //Linq
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion=await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;  
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion= await dbContext.Regions.FirstOrDefaultAsync(x => x.Id==id);
            if (existingRegion == null)
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion); //Remove doesn't have Async version
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
