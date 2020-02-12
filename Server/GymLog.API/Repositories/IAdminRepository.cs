using System.Collections.Generic;
using System.Threading.Tasks;
using GymLog.API.DTOs;
using GymLog.API.Entities;

namespace GymLog.API.Repositories
{
    public interface IAdminRepository
    {
        Task<User> GetUser(int id);
        Task UpdateAsync(User entity);
        Task<User> GetUserWithRoles(int id);
        Task<ICollection<UserWithRoleDto>> GetUsersWithRoles();
    }
}