namespace GymLog.API.Entities
{
    public class WorkoutDaylog
    {
        public int WorkoutId { get; private set; }
        public Workout Workout { get; private set; }

        public int DaylogId { get; private set; }
        public Daylog Daylog { get; private set; }

    }
}
