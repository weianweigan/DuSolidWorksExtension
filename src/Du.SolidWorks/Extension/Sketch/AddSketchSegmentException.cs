using System;
using System.Runtime.Serialization;

namespace Du.SolidWorks.Extension
{
    public class AddSketchSegmentException : Exception
    {
        public AddSketchSegmentException()
        {
        }

        public AddSketchSegmentException(string message) : base(message)
        {
        }

        public AddSketchSegmentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AddSketchSegmentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
