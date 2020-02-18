using GymLog.API.Data;
using GymLog.API.DTOs;
using GymLog.API.Entities;
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

        //public async Task<Daylog> GetDaylog(int userId, DateTime date)
        //    => await _ctx.Daylogs
        //    .Where(x => x.UserId == userId && x.Date == date)
        //    .Include(d => d.Workouts)
        //        .ThenInclude(w => w.Exercise)
        //    .Include(d => d.Workouts)
        //        .ThenInclude(w => w.Sets.OrderBy(x => x.Number))
        //    .FirstOrDefaultAsync();


        public async Task<DaylogDto> GetDaylog(int userId, DateTime date)
        {
            return await _ctx.Daylogs
                .Where(d => d.UserId == userId && d.Date == date)
                .Select(d => new DaylogDto
                {
                    Date = d.Date,
                    Workouts = d.Workouts.Select(w => new WorkoutDto
                    {
                        ExerciseId = w.ExerciseId,
                        ExerciseName = w.Exercise.Name,
                        Sets = w.Sets.Select(s => new SetDto
                        {
                            Number = s.Number,
                            Reps = s.Reps,
                            Weight = s.Weight
                        }).OrderBy(s => s.Number).ToList()
                    }).ToList()
                }).FirstOrDefaultAsync();
        }
    }
}
