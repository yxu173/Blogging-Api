namespace Application.Exceptions.TagExceptions;

public class GetTagsEx : ApplicationException
{
    public GetTagsEx()
    {
    }

    public GetTagsEx(string message) : base(message)
    {
    }

    public GetTagsEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}