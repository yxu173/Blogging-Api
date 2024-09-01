namespace Application.Exceptions.IdentityExceptions;

public class CreateUserProfileEx : ApplicationException
{
    internal CreateUserProfileEx()
    {
    }

    internal CreateUserProfileEx(string message) : base(message)
    {
    }

    internal CreateUserProfileEx(string message, Exception inner) : base(message, inner)
    {
    }
}