﻿using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Routine : AuditableEntity
    {
        public string Name { get; set; }
        public int Frequency { get; set; }
        public string Description { get; set; }
        public bool IsCurrent { get; set; }

        public int UserId { get; private set; }
        public virtual User User { get; private set; }
        public ICollection<DayWorkout> DayWorkouts { get; set; }

        public Routine()
        {

        }

        public Routine(string name, int frequency, string description, bool current, User user, ICollection<DayWorkout> dayWorkouts)
        {
            Name = name;
            Frequency = frequency;
            Description = description;
            IsCurrent = current;
            User = user;
            DayWorkouts = dayWorkouts;
        }
    }
}