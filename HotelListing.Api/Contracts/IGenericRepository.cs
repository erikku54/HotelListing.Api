namespace HotelListing.Api.Contracts;

public interface IGenericRepository<T>
    where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int? id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);

    Task<bool> Exists(int id);
}
