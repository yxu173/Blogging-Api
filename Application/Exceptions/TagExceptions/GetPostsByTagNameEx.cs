namespace Application.Exceptions.TagExceptions;

public class GetPostsByTagNameEx : ApplicationException
{
    public GetPostsByTagNameEx()
    {
    }

    public GetPostsByTagNameEx(string message) : base(message)
    {
    }

    public GetPostsByTagNameEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}