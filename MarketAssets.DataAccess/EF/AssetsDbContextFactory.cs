using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace MarketAssets.DataAccess.EF
{
    public class AssetsDbContextFactory : IDesignTimeDbContextFactory<AssetsDbContext>
    {
        public AssetsDbContextFactory()
        {

        }
        public AssetsDbContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable("PostgresConnectionString");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("The connection string was not set in the 'PostgresConnectionString' environment variable.");
            var options = new DbContextOptionsBuilder<AssetsDbContext>()
               .UseNpgsql(connectionString)
               .Options;
            return new AssetsDbContext(options);
        }
    }
}
