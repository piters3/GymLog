using GymLog.API.Exceptions;
using System;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Daylog : EntityBase
    {
        public DateTime Date { get; private set; }
        public User User { get; private set; }

        public IEnumerable<WorkoutDaylog> WorkoutDaylogs { get; set; }

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
