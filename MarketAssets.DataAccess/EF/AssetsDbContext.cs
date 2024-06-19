using MarketAssets.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace MarketAssets.DataAccess.EF
{
    public class AssetsDbContext : DbContext
    {
        public DbSet<MarketAsset> Assets { get; set; }
        public DbSet<AssetProvider> Providers { get; set; }
        public AssetsDbContext(DbContextOptions<AssetsDbContext> options) : base(options)
        {

        }
        public AssetsDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarketAsset>()
                .HasIndex(x => x.Symbol)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
