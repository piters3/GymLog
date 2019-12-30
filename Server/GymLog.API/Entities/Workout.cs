﻿using GymLog.API.Exceptions;
using System.Collections.Generic;

namespace GymLog.API.Entities
{
    public class Workout : EntityBase
    {
        public int Sets { get; private set; }
        public int Reps { get; private set; }
        public int Weight { get; private set; }
        public Exercise Exercise { get; private set; }
        public User User { get; private set; }

        public IEnumerable<WorkoutDaylog> WorkoutDaylogs { get; private set; }

        private Workout()
        {

        }

        public Workout(int sets, int reps, int weight, User user, Exercise exercise)
        {
            SetSets(sets);
            SetReps(reps);
            SetWeight(weight);
            SetUser(user);
            SetExercise(exercise);
        }

        private void SetSets(int sets)
        {
            if (sets <= 0)
            {
                throw new GymLogException(ExceptionCode.InvalidNumber, "Number of sets cannot be zero or negative.");
            }

            Sets = sets;
            SetUpdatedDate();
        }

        private void SetReps(int reps)
        {
            if (reps <= 0)
            {
                throw new GymLogException(ExceptionCode.InvalidNumber, "Number of reps cannot be zero or negative.");
            }

            Reps = reps;
            SetUpdatedDate();
        }

        private void SetWeight(int weight)
        {
            if (weight <= 0)
            {
                throw new GymLogException(ExceptionCode.InvalidNumber, "Number of sets cannot be zero or negative.");
            }

            Weight = weight;
            SetUpdatedDate();
        }

        private void SetUser(User user)
        {
            if (user is null)
                throw new GymLogException(ExceptionCode.NullReference, "Workout user cannot be null.");

            User = user;
            SetUpdatedDate();
        }

        private void SetExercise(Exercise exercise)
        {
            if (exercise is null)
                throw new GymLogException(ExceptionCode.NullReference, "Workout exercise cannot be null.");

            Exercise = exercise;
            SetUpdatedDate();
        }
    }
}
