namespace BitCoind.Exceptions
{
    public class ApiError
    {
        public ApiError(
            ApiErrorCode code,
            string message
        )
        {
            Code = code;
#if DEBUG
            Message = message;
#else
            Message = "Internal server error"; 
#endif
        }

        public ApiErrorCode Code { get; }

        public string Message { get; }
    }
}