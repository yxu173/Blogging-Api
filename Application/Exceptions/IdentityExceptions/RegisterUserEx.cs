namespace Application.Exceptions.IdentityExceptions;

public class RegisterUserEx : ApplicationException
{
    internal RegisterUserEx()
    {
    }

    internal RegisterUserEx(string message) : base(message)
    {
    }

    internal RegisterUserEx(string message, Exception inner) : base(message, inner)
    {
    }
}