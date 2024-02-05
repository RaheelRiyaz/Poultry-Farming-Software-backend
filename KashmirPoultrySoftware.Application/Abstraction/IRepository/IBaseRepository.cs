using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KashmirPoultrySoftware.Application.Abstraction.IRepository
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<int> AddAsync(T entity);
        Task<int> AddRangeAsync(List<T> entities);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteRangeAsync(List<T> entities);
        Task<int> DeleteRangeAsync(List<Guid> ids);
        Task<int> DeleteAsync(Guid id);
        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> expression);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression);

    }
}
