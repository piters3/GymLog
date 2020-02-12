using GymLog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public interface IUserRepository
    {
        Task<ICollection<Daylog>> GetUserDaylogs(int userId);
    }
}
