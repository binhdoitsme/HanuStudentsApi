namespace HanuEdmsApi.Exceptions
{
    public sealed class MissingAuthTokenException : UserFriendlyException
    {
        public MissingAuthTokenException() : base("The authToken is required to use this functionality!") { }
    }
}
