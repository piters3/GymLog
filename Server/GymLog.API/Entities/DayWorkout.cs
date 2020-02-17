using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class DayWorkout : AuditableEntity
    {
        public string Name { get; set; }
        public Day Day { get; set; }

        public ICollection<Workout> Workouts { get; set; }

        public DayWorkout()
        {

        }

        public DayWorkout(string name, Day day, ICollection<Workout> workouts)
        {
            Name = name;
            Day = day;
            Workouts = workouts;
        }
    }

    public enum Day
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}

