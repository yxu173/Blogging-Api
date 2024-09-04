namespace Application.Exceptions.RoleExceptions;

public class RoleCreationEx : ApplicationException
{
    public RoleCreationEx()
    {
    }

    public RoleCreationEx(string message) : base(message)
    {
    }

    public RoleCreationEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}