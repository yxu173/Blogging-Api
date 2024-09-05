namespace Application.Exceptions.PostExceptions;

public class DeleteCommentEx : ApplicationException
{
    public DeleteCommentEx()
    {
    }

    public DeleteCommentEx(string message) : base(message)
    {
    }

    public DeleteCommentEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}