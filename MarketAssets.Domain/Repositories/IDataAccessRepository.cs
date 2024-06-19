namespace MarketAssets.Domain.Repositories
{
    public interface IDataAccessRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity);
        Task InsertAsync(IEnumerable<T> entitySet);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
