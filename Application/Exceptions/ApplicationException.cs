namespace Application.Exceptions;

public class ApplicationException : Exception
{
    internal ApplicationException()
    {
        ValidationErrors = new List<string>();
    }

    internal ApplicationException(string message) : base(message)
    {
        ValidationErrors = new List<string>();
    }

    internal ApplicationException(string message, Exception inner) : base(message, inner)
    {
        ValidationErrors = new List<string>();
    }

    public List<string> ValidationErrors { get; }
}