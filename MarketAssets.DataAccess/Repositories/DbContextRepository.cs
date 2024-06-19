using MarketAssets.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MarketAssets.DataAccess.Repositories
{
    public class DbContextRepository<TContext, TEntity> : IDataAccessRepository<TEntity>
            where TEntity : class
            where TContext : DbContext
    {
        protected readonly TContext DbContext;
        protected readonly DbSet<TEntity> Entities;
        public DbContextRepository(TContext dbContext)
        {
            if (dbContext is null)
                throw new ArgumentNullException(nameof(dbContext));
            this.DbContext = dbContext;
            this.Entities = DbContext.Set<TEntity>();
            if (this.Entities is null)
                throw new ArgumentException($"No entity set of {typeof(TEntity)} was found in the db context");
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.Entities.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await this.Entities.FindAsync(id);
        }
        public virtual async Task InsertAsync(IEnumerable<TEntity> entitySet)
        {
            try
            {
                this.Entities.AddRange(entitySet);
                await this.DbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public virtual async Task InsertAsync(TEntity entity)
        {
            this.Entities.Add(entity);
            await this.DbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            this.Entities.Update(entity);
            await this.DbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            this.Entities.Remove(entity);
            await this.DbContext.SaveChangesAsync();
        }
    }
}
