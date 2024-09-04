namespace Domain.Exceptions;

public sealed class RoleException : DomainException
{
    public RoleException()
    {
    }

    public RoleException(string message) : base(message)
    {
    }

    public RoleException(string message, Exception inner) : base(message, inner)
    {
    }
}