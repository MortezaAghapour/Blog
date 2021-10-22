using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Shared.Enums.Exceptions;

namespace Blog.Shared.Exceptions
{
    public class NullArgumentException:AppException
    {
        public NullArgumentException()
            : base(CustomStatusCodes.ArgumentNull)
        {
        }

        public NullArgumentException(string message)
            : base(CustomStatusCodes.ArgumentNull, message)
        {
        }

        public NullArgumentException(object additionalData)
            : base(CustomStatusCodes.ArgumentNull, additionalData)
        {
        }

        public NullArgumentException(string message, object additionalData)
            : base(CustomStatusCodes.ArgumentNull, message, additionalData)
        {
        }

        public NullArgumentException(string message, Exception exception)
            : base(CustomStatusCodes.ArgumentNull, message, exception)
        {
        }

        public NullArgumentException(string message, Exception exception, object additionalData)
            : base(CustomStatusCodes.BadRequest, message, exception, additionalData)
        {
        }
    }
}
