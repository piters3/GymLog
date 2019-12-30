using GymLog.API.Exceptions;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Exercise : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Equipment Equipment { get; private set; }
        public Muscle Muscle { get; private set; }

        public IEnumerable<Workout> Workouts { get; private set; }

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
            SetUpdatedDate();
        }

        private void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
                throw new GymLogException(ExceptionCode.EmptyProperty, "Exercise description cannot be empty.");

            Description = description.Trim().ToLowerInvariant();
            SetUpdatedDate();
        }

        private void SetEquipment(Equipment equipment)
        {
            if (equipment is null)
                throw new GymLogException(ExceptionCode.NullReference, "Exercise equipment cannot be null.");

            Equipment = equipment;
            SetUpdatedDate();
        }

        private void SetMuscle(Muscle muscle)
        {
            if (muscle is null)
                throw new GymLogException(ExceptionCode.NullReference, "Exercise muscle cannot be null.");

            Muscle = muscle;
            SetUpdatedDate();
        }
    }
}
