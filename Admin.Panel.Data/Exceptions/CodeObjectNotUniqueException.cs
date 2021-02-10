using System;
using System.Runtime.Serialization;

namespace Admin.Panel.Data.Exceptions
{
    public class CodeObjectNotUniqueException: Exception
    {
        public CodeObjectNotUniqueException()
        {
        }

        protected CodeObjectNotUniqueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CodeObjectNotUniqueException(string? message) : base(message)
        {
        }

        public CodeObjectNotUniqueException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}