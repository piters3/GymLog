using GymLog.API.Exceptions;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Equipment : BaseEntity
    {
        public string Name { get; private set; }

        #region Relationships
        public virtual ICollection<Exercise> Exercises { get; private set; }
        #endregion

        public Equipment(string name)
        {
            SetName(name);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new GymLogException(ExceptionCode.EmptyProperty, "Equipment name cannot be empty.");

            Name = name.Trim();
        }
    }
}
