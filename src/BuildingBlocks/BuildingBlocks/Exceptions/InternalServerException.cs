namespace BuildingBlocks.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string message) : base(message) 
        {
            Details = string.Empty;
        }
        public InternalServerException(string message, string details) : this(message)
        {
            Details = details;
        }

        public string Details { get; }
    }
}
