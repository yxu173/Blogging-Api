namespace Application.Exceptions.IdentityExceptions;

public class UpdateEmailEx : ApplicationException
{
    internal UpdateEmailEx()
    {
    }

    internal UpdateEmailEx(string message) : base(message)
    {
    }

    internal UpdateEmailEx(string message, Exception inner) : base(message, inner)
    {
    }
}