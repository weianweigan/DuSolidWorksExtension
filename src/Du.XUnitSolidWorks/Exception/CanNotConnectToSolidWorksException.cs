using System;
using System.Runtime.Serialization;

namespace Du.XUnitSolidWorks
{
    [Serializable]
    internal class CanNotConnectToSolidWorksException : Exception
    {
        public CanNotConnectToSolidWorksException()
        {
        }

        public CanNotConnectToSolidWorksException(string message) : base(message)
        {
        }

        public CanNotConnectToSolidWorksException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CanNotConnectToSolidWorksException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}