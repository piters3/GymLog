using GymLog.API.Data;
using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public class ExercisesRepository : IExercisesRepository
    {
        private readonly DataContext _ctx;

        public ExercisesRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ICollection<Exercise>> GetAllAsync()
            => await _ctx.Exercises
            .Include(x => x.MainMuscle)
            .Include(x => x.Equipment)
            .ToListAsync();

        public async Task<Exercise> GetAsync(int id)
            => await _ctx.Exercises
            .Where(x => x.Id == id)
            .Include(x => x.MainMuscle)
            .Include(x => x.Equipment)
            .FirstOrDefaultAsync();
    }
}
