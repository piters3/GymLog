using System;
using System.Collections.Generic;
using GymLog.API.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace GymLog.API.Entities
{
    public class User : IdentityUser<int>, IIdentifiable
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public bool Enabled { get; private set; }
        public Gender Gender { get; private set; }
        public int Weight { get; private set; }
        public int Height { get; private set; }
        public DateTime RegisterDate { get; private set; }

        #region Navigation fields
        public virtual ICollection<UserRole> UserRoles { get; private set; }
        public virtual ICollection<Workout> Workouts { get; private set; }
        public virtual ICollection<Daylog> Daylogs { get; private set; }
        #endregion  

        public User()
        {
            Enabled = true;
            RegisterDate = DateTime.UtcNow;
        }

        public User(string name, string surname, int weight, int height, Gender gender)
        {
            SetName(name);
            SetSurname(surname);
            SetWeight(weight);
            SetHeight(height);
            Gender = gender;
            Enabled = true;
            RegisterDate = DateTime.UtcNow;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new GymLogException(ExceptionCode.EmptyProperty, "User name cannot be empty.");

            Name = name.Trim().ToLowerInvariant();
        }

        private void SetSurname(string surmame)
        {
            if (string.IsNullOrEmpty(surmame))
                throw new GymLogException(ExceptionCode.EmptyProperty, "User surname cannot be empty.");

            Surname = surmame.Trim().ToLowerInvariant();
        }

        private void SetWeight(int weight)
        {
            if (weight <= 0)
                throw new GymLogException(ExceptionCode.InvalidNumber, "Weight cannot be zero or negative.");

            Weight = weight;
        }

        private void SetHeight(int height)
        {
            if (height <= 0)
                throw new GymLogException(ExceptionCode.InvalidNumber, "Height cannot be zero or negative.");

            Height = height;
        }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
