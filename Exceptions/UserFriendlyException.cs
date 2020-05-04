namespace HanuEdmsApi.Exceptions
{
    public class UserFriendlyException
    {
        public string Message { get; set; }

        public UserFriendlyException(string message) => Message = message;
    }
}
