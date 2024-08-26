namespace Domain.Exceptions;

public class DomainException : Exception
{
    internal DomainException()
    {
        ValidationErrors = new List<string>();
    }

    internal DomainException(string message) : base(message)
    {
        ValidationErrors = new List<string>();
    }

    internal DomainException(string message, Exception inner) : base(message, inner)
    {
        ValidationErrors = new List<string>();
    }
    public List<string> ValidationErrors { get;  }
}