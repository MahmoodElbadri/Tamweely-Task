using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Tamweely.Application.Interfaces;
using Tamweely.Domain.Exceptions;
using Tamweely.Infrastructure.Data;

namespace Tamweely.Infrastructure.Repositories;

public class GenericRepository<T>(TamweelyDbContext db) : IGenericRepository<T> where T : class
{
    public async Task<T> AddAsync(T entity)
    {
        await db.Set<T>().AddAsync(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
       var entity = await db.Set<T>().FindAsync(id);
        if(entity != null)
        {
            db.Set<T>().Remove(entity);
            await db.SaveChangesAsync();
            return;
        }
        throw new NotFoundException(typeof(T).Name, id.ToString());
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> match, string[] includes = null)
    {
      var query = db.Set<T>().AsQueryable();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return await query.Where(match).ToListAsync();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await db.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var entity = await db.Set<T>().FindAsync(id);
        return entity;
    }

    public async Task<T?> UpdateAsync(T entity)
    {
        db.Set<T>().Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }
}
