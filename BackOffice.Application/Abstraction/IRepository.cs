namespace BackOffice.Application.Abstraction
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T?> GetByNameAsync(string name);
        Task<bool> AddAsync(T entity);
        Task<List<T>> GetAllAsync();
    }
}

