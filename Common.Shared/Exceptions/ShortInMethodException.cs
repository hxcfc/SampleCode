namespace Common.Shared.Exceptions
{
    public class ShortInMethodException : Exception
    {
        public ShortInMethodException(string? method, string? error) : base($"{method} - {error}")
        {
        }
    }
}