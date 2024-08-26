namespace Domain.Exceptions;

public class CommentException : DomainException
{
    internal CommentException() { }
    internal CommentException(string message) : base(message) { }

    internal CommentException(string message, Exception inner) : base(message, inner) { }
}