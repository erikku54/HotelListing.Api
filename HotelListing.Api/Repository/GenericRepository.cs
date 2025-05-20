using HotelListing.Api.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Api.Repository;

public class GenericRepository<T> : IGenericRepository<T>
    where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int? id)
    {
        if (id is null)
        {
            return null;
        }

        return await _dbSet.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> Exists(int id)
    {
        return await _dbSet.AnyAsync(e => EF.Property<int>(e, "Id") == id);
    }
}
