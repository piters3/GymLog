using System.Collections.Generic;
using System.Threading.Tasks;
using GymLog.API.Entities;

namespace GymLog.API.Repositories
{
    public interface IUsersRepository
    {
        Task SaveAllAsync();
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserWithRoles(int id);
    }
}