using MarketAssets.DataAccess.EF;
using MarketAssets.Domain.Models;

namespace MarketAssets.DataAccess.Repositories
{
    public class MarketAssetsRepository : DbContextRepository<AssetsDbContext, MarketAsset>
    {
        public MarketAssetsRepository(AssetsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
