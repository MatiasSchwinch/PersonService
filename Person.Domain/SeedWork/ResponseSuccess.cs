namespace Person.Domain.SeedWork
{
    public class ResponseSuccess
    {
        public string? Message { get; private set; }

        public ResponseSuccess(string? message)
        {
            Message = message;
        }

        public ResponseSuccess()
        {

        }
    }
}
