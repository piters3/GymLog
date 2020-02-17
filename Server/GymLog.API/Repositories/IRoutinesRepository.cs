using GymLog.API.Entities;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public interface IRoutinesRepository
    {
        Task<Routine> GetRoutines(int userId);
    }
}
