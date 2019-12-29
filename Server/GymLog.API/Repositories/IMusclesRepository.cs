using GymLog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public interface IMusclesRepository
    {
        Task<Muscle> GetAsync(int id);
        Task<ICollection<Muscle>> GetAllAsync();
        Task DeleteAsync(Muscle entity);
        Task AddAsync(Muscle entity);
        Task UpdateAsync(Muscle entity);
    }
}
