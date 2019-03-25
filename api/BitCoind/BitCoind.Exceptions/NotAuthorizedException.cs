namespace BitCoind.Exceptions
{
    public class NotAuthorizedException : ApiException
    {
        public NotAuthorizedException(
            string token,
            ApiErrorCode code
        )
            : base($"Unauthorized token {token}", code)
        {
            Token = token;
        }

        public string Token { get; }
    }
}