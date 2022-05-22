namespace Person.Domain.SeedWork
{
    public class ResponseException
    {
        public string? Message { get; init; }
        public string? Type { get; init; }
        public string? StackTrace { get; init; }
        public Exception? InnerException { get; init; }
        public ResponseException(string? message, string? type, string? stackTrace, Exception? innerException)
        {
            Message = message;
            Type = type;
            StackTrace = stackTrace;
            InnerException = innerException;
        }

        public ResponseException(string? message, string? type)
        {
            Message = message;
            Type = type;
        }
    }
}
