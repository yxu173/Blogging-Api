namespace Application.Exceptions.PostExceptions;

public class AddCommentEx : ApplicationException
{
    public AddCommentEx()
    {
    }

    public AddCommentEx(string message) : base(message)
    {
    }

    public AddCommentEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}