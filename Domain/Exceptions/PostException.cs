namespace Domain.Exceptions;

public class PostException : DomainException
{
    internal PostException() { }
    internal PostException(string message) : base(message) { }

    internal PostException(string message, Exception inner) : base(message, inner) { }
}