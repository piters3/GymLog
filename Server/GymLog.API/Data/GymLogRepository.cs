using System.Collections.Generic;
using System.Threading.Tasks;
using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymLog.API.Data
{
    public class GymLogRepository : IGymLogRepository
    {
        private readonly DataContext _context;
        public GymLogRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }

        //public async Task<User> GetUser(int id, bool isCurrentUser)
        //{
        //    var query = _context.Users.AsQueryable();

        //    if (isCurrentUser)
        //        query = query.IgnoreQueryFilters();

        //    var user = await query.FirstOrDefaultAsync(u => u.Id == id);

        //    return user;
        //}
    }
}
