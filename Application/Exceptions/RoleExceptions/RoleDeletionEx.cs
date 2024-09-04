namespace Application.Exceptions.RoleExceptions;

public class RoleDeletionEx : ApplicationException
{
    public RoleDeletionEx()
    {
    }

    public RoleDeletionEx(string message) : base(message)
    {
    }

    public RoleDeletionEx(string message, Exception innerException) : base(message, innerException)
    {
    }
}