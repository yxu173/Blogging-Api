namespace Application.Enums;

public enum ErrorCode
{
    NotFound = 404,
    ServerError = 500,

//Validation errors should be in the range 100 - 199
    ValidationError = 101,
    FriendRequestValidationError = 102,

//Infrastructure errors should be in the range 200-299
    IdentityCreationFailed = 202,
    DatabaseOperationException = 203,

//Application errors should be in the range 300 - 399


    // User errors should be in the range 306-310
    UserAlreadyExists = 305,
    UserDoesNotExist = 306,
    IncorrectPassword = 307,
    WrongPassword = 308,


    UnknownError = 999,
}