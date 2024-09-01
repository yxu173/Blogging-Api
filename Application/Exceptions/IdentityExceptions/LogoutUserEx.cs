namespace Application.Exceptions.IdentityExceptions;

public class LogoutUserEx : ApplicationException
{
    internal LogoutUserEx()
    {
    }

    internal LogoutUserEx(string message) : base(message)
    {
    }

    internal LogoutUserEx(string message, Exception inner) : base(message, inner)
    {
    }
}