namespace Domain.Exceptions;

public sealed class UserException : DomainException
{
    internal UserException() { }
    internal UserException(string message) : base(message) { }

    internal UserException(string message, Exception inner) : base(message, inner) { }
}