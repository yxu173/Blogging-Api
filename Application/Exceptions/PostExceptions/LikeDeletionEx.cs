namespace Application.Exceptions.PostExceptions;

public class LikeDeletionEx : ApplicationException
{
    public LikeDeletionEx()
    {
    }

    public LikeDeletionEx(string message) : base(message)
    {
    }

    public LikeDeletionEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}