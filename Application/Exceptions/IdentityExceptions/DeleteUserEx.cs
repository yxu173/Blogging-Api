namespace Application.Exceptions.IdentityExceptions;

public class DeleteUserEx : ApplicationException
{
    internal DeleteUserEx()
    {
    }

    internal DeleteUserEx(string message) : base(message)
    {
    }

    internal DeleteUserEx(string message, Exception inner) : base(message, inner)
    {
    }
}