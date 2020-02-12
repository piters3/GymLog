using GymLog.API.Data;
using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public class MusclesRepository : IMusclesRepository
    {
        private readonly DataContext _ctx;

        public MusclesRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ICollection<Muscle>> GetAllAsync()
            => await _ctx.Muscles.ToListAsync();

        public async Task<Muscle> GetAsync(int id)
            => await _ctx.Muscles.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task AddAsync(Muscle entity)
        {
            await _ctx.Muscles.AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Muscle entity)
        {
            _ctx.Muscles.Update(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(Muscle entity)
        {
            _ctx.Muscles.Remove(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}
