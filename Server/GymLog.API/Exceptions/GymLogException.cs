using System;
using System.Runtime.Serialization;

namespace GymLog.API.Exceptions
{

    [Serializable]
    public class GymLogException : Exception
    {
        public ExceptionCode Code { get; }

        public GymLogException()
        {
        }

        public GymLogException(ExceptionCode code, string message) : base(message)
        {
            Code = code;
        }

        public GymLogException(string message) : base(message)
        {
        }

        public GymLogException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GymLogException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
