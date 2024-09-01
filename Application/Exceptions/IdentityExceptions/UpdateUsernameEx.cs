namespace Application.Exceptions.IdentityExceptions;

public class UpdateUsernameEx : ApplicationException
{
    internal UpdateUsernameEx()
    {
    }

    internal UpdateUsernameEx(string message) : base(message)
    {
    }

    internal UpdateUsernameEx(string message, Exception inner) : base(message, inner)
    {
    }
}