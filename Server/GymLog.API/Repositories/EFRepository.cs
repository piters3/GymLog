using GymLog.API.Data;
using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public class EFRepository<T> : IEFRepository<T> where T : EntityBase
    {
        protected readonly DataContext Context;
        public EFRepository(DataContext context)
        {
            Context = context;
        }

        public async Task<ICollection<T>> GetAllAsync()
            => await Context.Set<T>().ToListAsync();

        public async Task<T> GetAsync(int id)
            => await GetAsync(e => e.Id == id);

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
            => await Context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await Context.Set<T>().Where(predicate).ToListAsync();

        public async Task AddAsync(T entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
            => await Context.Set<T>().CountAsync();
    }
}