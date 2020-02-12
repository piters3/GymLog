namespace GymLog.API.DTOs
{
    public class WorkoutsDto
    {
        public int Sets { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
    }
}
