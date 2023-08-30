namespace Common.Shared.Exceptions
{
    public class InMethodException : Exception
    {
        public InMethodException(string? method, string? error) : base($"Error in method {method}. Error: {error}.")
        {
        }
    }
}