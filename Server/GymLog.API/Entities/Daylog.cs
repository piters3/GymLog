using GymLog.API.Exceptions;
using System;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Daylog : AuditableEntity
    {
        public DateTime Date { get; private set; }

        #region Relationships
        public int UserId { get; private set; }
        public virtual User User { get; private set; }
        public virtual ICollection<WorkoutDaylog> WorkoutDaylogs { get; private set; }
        #endregion

        private Daylog()
        {

        }

        public Daylog(DateTime date, User user)
        {
            Date = date;
            SetUser(user);
        }

        private void SetUser(User user)
        {
            if (user is null)
                throw new GymLogException(ExceptionCode.NullReference, "Daylog user cannot be null.");

            User = user;
        }
    }
}
