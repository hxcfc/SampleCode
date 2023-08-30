namespace Common.Shared.Exceptions
{
    public class DbBadRequestException : Exception
    {
        public DbBadRequestException(string? message) : base($"Error in query {message}")
        {
        }
    }
}