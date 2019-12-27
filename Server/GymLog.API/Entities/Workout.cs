using GymLog.API.Exceptions;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Workout : EntityBase
    {
        public int Sets { get; private set; }
        public int Reps { get; private set; }
        public int Weight { get; private set; }

        public IEnumerable<WorkoutDaylog> WorkoutDaylogs { get; private set; }

        public Workout(int sets, int reps, int weight)
        {
            SetSets(sets);
            SetReps(reps);
            SetWeight(weight);
        }

        private void SetSets(int sets)
        {
            if (sets <= 0)
            {
                throw new GymLogException(ExceptionCode.InvalidNumber, "Number of sets cannot be zero or negative.");
            }

            Sets = sets;
            SetUpdatedDate();
        }

        private void SetReps(int reps)
        {
            if (reps <= 0)
            {
                throw new GymLogException(ExceptionCode.InvalidNumber, "Number of reps cannot be zero or negative.");
            }

            Reps = reps;
            SetUpdatedDate();
        }

        private void SetWeight(int weight)
        {
            if (weight <= 0)
            {
                throw new GymLogException(ExceptionCode.InvalidNumber, "Number of sets cannot be zero or negative.");
            }

            Weight = weight;
            SetUpdatedDate();
        }
    }
}
