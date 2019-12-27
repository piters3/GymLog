using System;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Daylog : EntityBase
    {
        public DateTime Date { get; private set; }

        public IEnumerable<WorkoutDaylog> WorkoutDaylogs { get; private set; }

        public Daylog()
        {

        }
    }
}
