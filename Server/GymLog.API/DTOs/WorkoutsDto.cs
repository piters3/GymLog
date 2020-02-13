namespace GymLog.API.DTOs
{
    public class WorkoutDto
    {
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
    }
}
