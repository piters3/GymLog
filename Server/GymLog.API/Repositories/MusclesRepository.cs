using GymLog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public class MusclesRepository : IMusclesRepository
    {
        private readonly IEFRepository<Muscle> _repository;

        public MusclesRepository(IEFRepository<Muscle> repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Muscle>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Muscle> GetAsync(int id)
            => await _repository.GetAsync(id);

        public async Task AddAsync(Muscle entity)
            => await _repository.AddAsync(entity);

        public async Task UpdateAsync(Muscle entity)
            => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(Muscle entity)
            => await _repository.DeleteAsync(entity);
    }
}
