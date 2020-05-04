namespace HanuEdmsApi.Exceptions
{
    public sealed class ServerFailedException : UserFriendlyException
    {
        public ServerFailedException() : base("An error occurred, please check the server log for details!") { }
    }
}
