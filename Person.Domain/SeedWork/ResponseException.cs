namespace Person.Domain.SeedWork
{
    public class ResponseException
    {
        public string? Message { get; init; }
        public string? Type { get; init; }
        public ResponseException(string? message, string? type)
        {
            Message = message;
            Type = type;
        }
    }
}
