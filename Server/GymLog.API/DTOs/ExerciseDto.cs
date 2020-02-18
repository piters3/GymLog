using GymLog.API.Entities;

namespace GymLog.API.DTOs
{
    public class ExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ExerciseType Type { get; set; }
        public Difficulty Difficulty { get; set; }
        public string DetailedMuscle { get; set; }
        public string OtherMuscles { get; set; }
        public string EquipmentName { get; set; }
        public string MainMuscleName { get; set; }
    }
}
