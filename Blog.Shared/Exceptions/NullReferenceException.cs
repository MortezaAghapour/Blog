using System;
using Blog.Shared.Enums.Exceptions;

namespace Blog.Shared.Exceptions
{
    public class NullReferenceException:AppException
    {
        public NullReferenceException()
            : base(CustomStatusCodes.NullReference)
        {
        }

        public NullReferenceException(string message)
            : base(CustomStatusCodes.NullReference, message)
        {
        }

        public NullReferenceException(object additionalData)
            : base(CustomStatusCodes.NullReference, additionalData)
        {
        }

        public NullReferenceException(string message, object additionalData)
            : base(CustomStatusCodes.NullReference, message, additionalData)
        {
        }

        public NullReferenceException(string message, Exception exception)
            : base(CustomStatusCodes.NullReference, message, exception)
        {
        }

        public NullReferenceException(string message, Exception exception, object additionalData)
            : base(CustomStatusCodes.BadRequest, message, exception, additionalData)
        {
        }
    }
}
