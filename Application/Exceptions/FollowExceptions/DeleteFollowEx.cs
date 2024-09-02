namespace Application.Exceptions.FollowExceptions;

public class DeleteFollowEx : ApplicationException
{
    public DeleteFollowEx()
    {
    }

    public DeleteFollowEx(string message) : base(message)
    {
    }

    public DeleteFollowEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}