using System.Collections.Generic;

namespace GymLog.API.DTOs
{
    public class WorkoutDto
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public ICollection<SetDto> Sets { get; set; }
    }
}
