using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymLog.API.Data;
using GymLog.API.DTOs;
using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymLog.API.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _ctx;

        public AdminRepository(DataContext context)
        {
            _ctx = context;
        }

        public async Task<User> GetUser(int id)
            => await _ctx.Users.FirstOrDefaultAsync(u => u.Id == id);

        public async Task UpdateAsync(User entity)
        {
            _ctx.Users.Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task<User> GetUserWithRoles(int id)
            => await _ctx.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<ICollection<UserWithRoleDto>> GetUsersWithRoles()
            => await _ctx.Users.Select(user => new UserWithRoleDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = user.UserRoles.Select(x=>x.Role.Name).ToArray()

            }).ToListAsync();
    }
}
