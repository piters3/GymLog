using GymLog.API.Data;
using GymLog.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _ctx;

        public UserRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ICollection<Daylog>> GetUserDaylogs(int userId)
            => await _ctx.Daylogs
            //.Include(x => x.WorkoutDaylogs)
            //.ThenInclude(x => x.Workout)
            //.ThenInclude(x => x.Exercise)
            .ToListAsync();
    }
}
