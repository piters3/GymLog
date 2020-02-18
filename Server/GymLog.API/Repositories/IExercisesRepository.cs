using GymLog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public interface IExercisesRepository
    {
        Task<Exercise> GetAsync(int id);
        Task<ICollection<Exercise>> GetAllAsync();
    }
}
