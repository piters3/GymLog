using GymLog.API.Exceptions;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Workout : AuditableEntity
    {
        #region Relationships
        public int ExerciseId { get; }
        public virtual Exercise Exercise { get; private set; }
        public virtual ICollection<Set> Sets { get; private set; }

        #endregion  

        public Workout()
        {

        }

        public Workout(Exercise exercise, ICollection<Set> sets)
        {
            SetExercise(exercise);
            SetSets(sets);
        }

        private void SetExercise(Exercise exercise)
        {
            if (exercise is null)
                throw new GymLogException(ExceptionCode.NullReference, "Workout exercise cannot be null.");

            Exercise = exercise;
        }

        private void SetSets(ICollection<Set> sets)
        {
            if (sets == null || sets.Count == 0)
            {
                throw new GymLogException(ExceptionCode.EmptyCollection,
                    $"Cannot create an workout for an empty sets.");
            }

            Sets = sets;
        }
    }
}

