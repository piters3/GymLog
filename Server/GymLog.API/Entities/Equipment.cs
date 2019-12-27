using GymLog.API.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace GymLog.API.Entities
{
    public class Equipment : EntityBase
    {
        public string Name { get; private set; }
        public IEnumerable<Exercise> Exercises { get; private set; }

        public Equipment(string name)
        {
            SetName(name);
            //SetExcercises(exercises);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new GymLogException(ExceptionCode.EmptyProperty, "Equipment name cannot be empty.");

            Name = name.Trim().ToLowerInvariant();
            SetUpdatedDate();
        }

        private void SetExcercises(IEnumerable<Exercise> exercises)
        {
            if (exercises == null || !exercises.Any())
            {
                throw new GymLogException(ExceptionCode.EmptyCollection,
                    $"Cannot create an equipment for an empty excercises.");
            }
        }
    }
}
