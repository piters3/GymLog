using GymLog.API.Exceptions;

namespace GymLog.API.Entities
{
    public class Set : AuditableEntity
    {
        public int Number { get; private set; }
        public int Reps { get; private set; }
        public float Weight { get; private set; }

        public Set()
        {

        }

        public Set(int number, int reps, float weight)
        {
            SetNumber(number);
            SetReps(reps);
            SetWeight(weight);
        }

        private void SetNumber(int number)
        {
            if (number <= 0)
            {
                throw new GymLogException(ExceptionCode.InvalidNumber, "Number cannot be zero or negative.");
            }

            Number = number;
        }

        private void SetReps(int reps)
        {
            if (reps <= 0)
            {
                throw new GymLogException(ExceptionCode.InvalidNumber, "Number of reps cannot be zero or negative.");
            }

            Reps = reps;
        }

        private void SetWeight(float weight)
        {
            if (weight <= 0)
            {
                throw new GymLogException(ExceptionCode.InvalidNumber, "Number of sets cannot be zero or negative.");
            }

            Weight = weight;
        }
    }
}
