using System.Collections.Generic;
using System.Threading.Tasks;
using GymLog.API.Data;
using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymLog.API.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly DataContext _context;
        public UsersRepository(DataContext context)
        {
            _context = context;
        }

        public async Task SaveAllAsync()
            => await _context.SaveChangesAsync();

        public async Task<User> GetUser(int id)
            => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);


        public async Task<User> GetUserWithRoles(int id)
            => await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<IEnumerable<User>> GetUsers()
            => await _context.Users.ToListAsync();
    }
}
