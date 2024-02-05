using KashmirPoultrySoftware.Application.Abstraction.IRepository;
using KashmirPoultrySoftware.Domain.Entities;
using KashmirPoultrySoftware.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace KashmirPoultrySoftware.Persistence.Repository
{
    public class BaseRepository<T>: IBaseRepository<T> where T : BaseEntity,new ()
    {
        protected readonly KashmirPoultrySoftwareDbContext context;
        private readonly Microsoft.EntityFrameworkCore.DbSet<T> dbSet;
        public BaseRepository(KashmirPoultrySoftwareDbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }


        public async Task<int> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(List<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            dbSet.Remove(new T() { Id = id});
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(List<T> entities)
        {
            dbSet.RemoveRange(entities);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(List<Guid> ids)
        {
            List<T> entities = new List<T>();

            foreach (var id in ids)
            {
                entities.Add(new T() { Id = id });
            }

            dbSet.RemoveRange(entities);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> expression)
        {
            return await  dbSet.Where(expression).ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(() => dbSet);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
          return await dbSet.FindAsync(id);
        }

        public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await dbSet.AnyAsync(expression);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            return await context.SaveChangesAsync();
        }
    }
}
