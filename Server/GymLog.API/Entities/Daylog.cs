using GymLog.API.Exceptions;
using System;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Daylog : AuditableEntity, IUserId
    {
        public DateTime Date { get; private set; }

        #region Relationships
        public int UserId { get; set; }
        public virtual User User { get; private set; }
        public virtual ICollection<Workout> Workouts { get; private set; }
        #endregion

        private Daylog()
        {

        }

        public Daylog(DateTime date, ICollection<Workout> workouts, User user)
        {
            Date = date;
            SetUser(user);
            SetWorkouts(workouts);
        }

        private void SetUser(User user)
        {
            if (user is null)
                throw new GymLogException(ExceptionCode.NullReference, "Daylog user cannot be null.");

            User = user;
        }

        private void SetWorkouts(ICollection<Workout> workouts)
        {
            if (workouts == null || workouts.Count == 0)
            {
                throw new GymLogException(ExceptionCode.EmptyCollection,
                    $"Cannot create an equipment for an empty excercises.");
            }

            Workouts = workouts;
        }
    }
}
