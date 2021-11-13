using System;
using Blog.Shared.Enums.Exceptions;

namespace Blog.Shared.Exceptions
{
    public class BadRequestException:AppException
    {
        public BadRequestException()
            : base(CustomStatusCodes.BadRequest)
        {
        }

        public BadRequestException(string message)
            : base(CustomStatusCodes.BadRequest, message)
        {
        }

        public BadRequestException(object additionalData)
            : base(CustomStatusCodes.BadRequest, additionalData)
        {
        }

        public BadRequestException(string message, object additionalData)
            : base(CustomStatusCodes.BadRequest, message, additionalData)
        {
        }

        public BadRequestException(string message, Exception exception)
            : base(CustomStatusCodes.BadRequest, message, exception)
        {
        }

        public BadRequestException(string message, Exception exception, object additionalData)
            : base(CustomStatusCodes.BadRequest, message, exception, additionalData)
        {
        }
    }
}
