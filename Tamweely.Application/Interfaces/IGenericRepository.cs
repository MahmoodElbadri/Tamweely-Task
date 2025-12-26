using System.Linq.Expressions;

namespace Tamweely.Application.Interfaces;

public interface IGenericRepository<T> where T : class
{
    //For Reading
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(); //IEnumrable is good at reading cases 
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> match, string[] includes = null);

    //For Writing
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(int id);

}
