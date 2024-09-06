namespace Domain.Exceptions;

public class ReportException : DomainException
{
    public ReportException()
    {
    }

    public ReportException(string message) : base(message)
    {
    }
    
}