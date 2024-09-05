namespace Application.Exceptions.PostExceptions;

public class AddLikeEx : ApplicationException
{
    public AddLikeEx()
    {
    }

    public AddLikeEx(string message) : base(message)
    {
    }

    public AddLikeEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}