using System;
using System.Collections.Generic;

namespace GymLog.API.DTOs
{
    public class DaylogDto
    {
        public DateTime Date { get; set; }
        public ICollection<WorkoutDto> Workouts { get; set; }
    }
}
