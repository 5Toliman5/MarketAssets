using MarketAssets.DataAccess.EF;
using MarketAssets.Domain.Models;

namespace MarketAssets.DataAccess.Repositories
{
    public class AssetProvidersRepository : DbContextRepository<AssetsDbContext, AssetProvider>
    {
        public AssetProvidersRepository(AssetsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
