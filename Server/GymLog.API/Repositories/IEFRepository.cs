using GymLog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public interface IEFRepository<T> where T : EntityBase
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> CountAsync();
    }
}
