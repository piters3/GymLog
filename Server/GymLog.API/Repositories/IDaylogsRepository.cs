using GymLog.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymLog.API.Repositories
{
    public interface IDaylogsRepository
    {
        Task<ICollection<DateTime>> GetDaylogsDates(int userId, DateTime date);
        Task<Daylog> GetDaylog(int userId, DateTime date);
    }
}
