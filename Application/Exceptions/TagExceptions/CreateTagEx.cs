namespace Application.Exceptions.TagExceptions;

public class CreateTagEx : ApplicationException
{
    public CreateTagEx()
    {
    }

    public CreateTagEx(string message) : base(message)
    {
    }

    public CreateTagEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}