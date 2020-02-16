using GymLog.API.Exceptions;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Exercise : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        #region Relationships
        public int EquipmentId { get; set; }
        public int MuscleId { get; set; }
        public virtual Equipment Equipment { get; private set; }
        public virtual Muscle Muscle { get; private set; }
        public ICollection<Workout> Workouts { get; private set; }
        #endregion

        private Exercise()
        {

        }

        public Exercise(string name, string description, Muscle muscle, Equipment equipment)
        {
            SetName(name);
            SetDescription(description);
            SetMuscle(muscle);
            SetEquipment(equipment);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new GymLogException(ExceptionCode.EmptyProperty, "Exercise name cannot be empty.");

            Name = name.Trim().ToLowerInvariant();
        }

        private void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new GymLogException(ExceptionCode.EmptyProperty, "Exercise description cannot be empty.");

            Description = description.Trim().ToLowerInvariant();
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

            Muscle = muscle;
        }
    }
}
