﻿using GymLog.API.Exceptions;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Muscle : BaseEntity
    {
        public string Name { get; private set; }

        #region Relationships
        public virtual ICollection<Exercise> Exercises { get; private set; }
        #endregion  

        public Muscle(string name)
        {
            SetName(name);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new GymLogException(ExceptionCode.EmptyProperty, "Muscle name cannot be empty.");

            Name = name.Trim();
        }
    }
}
