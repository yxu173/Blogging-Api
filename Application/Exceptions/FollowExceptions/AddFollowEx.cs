namespace Application.Exceptions.FollowExceptions;

public class AddFollowEx : ApplicationException
{
    public AddFollowEx()
    {
    }

    public AddFollowEx(string message) : base(message)
    {
    }

    public AddFollowEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}