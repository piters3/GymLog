using GymLog.API.Exceptions;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public enum ExerciseType
    {
        Strength,
        Stretching,
        Powerlifting,
        Olympic
    }

    public enum Difficulty
    {
        Beginner,
        Intermediate,
        Expert
    }

    public class Exercise : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public ExerciseType Type { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public string DetailedMuscle { get; private set; }
        public string OtherMuscles { get; private set; }

        #region Relationships
        public int EquipmentId { get; set; }
        public int MuscleId { get; set; }
        public virtual Equipment Equipment { get; private set; }
        public virtual Muscle MainMuscle { get; private set; }
        public virtual ICollection<Workout> Workouts { get; private set; }
        #endregion

        private Exercise()
        {

        }

        public Exercise(string name, string description, ExerciseType type, Difficulty difficulty, Muscle muscle, Equipment equipment, string detailedMuscle = null, string otherMuscles = null)
        {
            SetName(name);
            SetDescription(description);
            SetMuscle(muscle);
            SetEquipment(equipment);
            Type = type;
            Difficulty = difficulty;
            DetailedMuscle = detailedMuscle;
            OtherMuscles = otherMuscles;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new GymLogException(ExceptionCode.EmptyProperty, "Exercise name cannot be empty.");

            Name = name.Trim();
        }

        private void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new GymLogException(ExceptionCode.EmptyProperty, "Exercise description cannot be empty.");

            Description = description.Trim();
        }

        private void SetEquipment(Equipment equipment)
        {
            if (equipment is null)
                throw new GymLogException(ExceptionCode.NullReference, "Exercise equipment cannot be null.");

            Equipment = equipment;
        }

        private void SetMuscle(Muscle muscle)
        {
            if (muscle is null)
                throw new GymLogException(ExceptionCode.NullReference, "Exercise muscle cannot be null.");

            MainMuscle = muscle;
        }
    }
}
