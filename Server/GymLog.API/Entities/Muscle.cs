using GymLog.API.Exceptions;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Muscle : EntityBase
    {
        public string Name { get; private set; }
        public IEnumerable<Exercise> Exercises { get; private set; }

        public Muscle(string name)
        {
            SetName(name);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new GymLogException(ExceptionCode.EmptyProperty, "Muscle name cannot be empty.");

            Name = name.Trim().ToLowerInvariant();
            SetUpdatedDate();
        }
    }
}
