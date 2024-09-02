namespace Application.Exceptions.TagExceptions;

public class DeleteTagEx : ApplicationException
{
    public DeleteTagEx()
    {
    }

    public DeleteTagEx(string message) : base(message)
    {
    }

    public DeleteTagEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}