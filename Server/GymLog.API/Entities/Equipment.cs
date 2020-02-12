using GymLog.API.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace GymLog.API.Entities
{
    public class Equipment : EntityBase
    {
        public string Name { get; private set; }

        #region Navigation fields
        public virtual ICollection<Exercise> Exercises { get; private set; }
        #endregion

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
