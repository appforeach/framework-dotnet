using System;
using System.Runtime.Serialization;

namespace AppForeach.Framework
{

    [Serializable]
    public class FrameworkException : Exception
    {
        public FrameworkException() { }

        public FrameworkException(string message) : base(message) { }

        public FrameworkException(string message, Exception inner) : base(message, inner) { }
        
        protected FrameworkException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
