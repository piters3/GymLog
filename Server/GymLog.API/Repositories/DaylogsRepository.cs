using GymLog.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public class DaylogsRepository : IDaylogsRepository
    {
        private readonly DataContext _ctx;

        public DaylogsRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ICollection<DateTime>> GetDaylogsDates(int userId, DateTime date)
            => await _ctx.Daylogs
            .Where(x => x.UserId == userId && x.Date.Year == date.Year && x.Date.Month == date.Month)
            .Select(x => x.Date)
            .ToListAsync();

    }
}
