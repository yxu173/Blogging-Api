namespace Application.Exceptions.PostExceptions;

public class CreatePostEx : ApplicationException
{
    public CreatePostEx()
    {
    }

    public CreatePostEx(string message) : base(message)
    {
    }

    public CreatePostEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}