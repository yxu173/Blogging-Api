namespace Application.Exceptions.IdentityExceptions;

public class LoginUserEx : ApplicationException
{
    internal LoginUserEx()
    {
    }

    internal LoginUserEx(string message) : base(message)
    {
    }

    internal LoginUserEx(string message, Exception inner) : base(message, inner)
    {
    }
}