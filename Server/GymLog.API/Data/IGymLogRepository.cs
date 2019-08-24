using System.Collections.Generic;
using System.Threading.Tasks;
using GymLog.API.Entities;

namespace GymLog.API.Data
{
    public interface IGymLogRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserWithRoles(int id);
    }
}