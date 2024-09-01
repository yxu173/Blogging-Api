namespace Application.Exceptions.IdentityExceptions;

public class UpdateUserProfileEx : ApplicationException
{
    internal UpdateUserProfileEx()
    {
    }

    internal UpdateUserProfileEx(string message) : base(message)
    {
    }

    internal UpdateUserProfileEx(string message, Exception inner) : base(message, inner)
    {
    }
}