using GymLog.API.Exceptions;

namespace GymLog.API.Entities
{
    public class WorkoutDaylog
    {
        public int WorkoutId { get; private set; }
        public Workout Workout { get; private set; }

        public int DaylogId { get; private set; }
        public Daylog Daylog { get; private set; }

        private WorkoutDaylog()
        {

        }

        public WorkoutDaylog(Workout workout, Daylog daylog)
        {
            if (workout is null)
                throw new GymLogException(ExceptionCode.NullReference, "Workout cannot be null.");

            Workout = workout;

            if (daylog is null)
                throw new GymLogException(ExceptionCode.NullReference, "Daylog cannot be null.");

            Daylog = daylog;
        }
    }
}
