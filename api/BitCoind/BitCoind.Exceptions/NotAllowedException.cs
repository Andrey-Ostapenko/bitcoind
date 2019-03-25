namespace BitCoind.Exceptions
{
    public class NotAllowedException : ApiException
    {
        public NotAllowedException(
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