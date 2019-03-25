using System;

namespace BitCoind.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(
            string messagge,
            ApiErrorCode code
        )
            : base(messagge)
        {
            Code = code;
        }

        public ApiErrorCode Code { get; }

        public ApiError AsError()
        {
            return new ApiError(Code, Message);
        }
    }
}