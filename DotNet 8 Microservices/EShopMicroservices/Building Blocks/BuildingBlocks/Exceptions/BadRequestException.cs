
namespace BuildingBlocks.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Details { get; }

        public BadRequestException(string message) : base(message)
        {
            Details = string.Empty;
        }

        public BadRequestException(string message, string details) : base(message)
        {
            Details = details;
        }
    }
}
