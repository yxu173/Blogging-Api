namespace Application.Enums;

public enum ErrorCode
{
    NotFound = 404,
    BadRequest = 400,
    ServerError = 500,

    // Identity error codes should be in the range 300 - 399
    UserProfileCreationFailed = 300,
    UserDeletionFailed = 301,
    UserLoginFailed = 302,
    UserLogoutFailed = 303,
    UserRegistrationFailed = 304,
    UpdateEmailFailed = 305,
    UpdateUsernameFailed = 306,
    UpdateUserProfileFailed = 307,
    UserAlreadyExists = 309,
    
    //Tag errors should be in the range 300 - 399
    TagCreationFailed = 310,
    TagDeletionFailed = 311,
    TagUpdateFailed = 312,
    TagDoesNotExist = 313,
    
    
    //Role errors should be in the range 300 - 399
    RoleCreationFailed = 320,
    RoleAlreadyExists = 321,
    RoleDoesNotExist = 322,
    RoleDeletionFailed = 323,
    RoleNotFound = 324,
    InvalidRoleName = 325,
    //Application errors should be in the range 300 - 399
    UserNotAuthorized = 300,
    PostNotAuthorized = 301,
    PostDoesNotExist = 302,
    PostAlreadyExists = 303,
    CommentNotAuthorized = 304,
    CommentDoesNotExist = 305,
    

//Application errors should be in the range 300 - 399
    CommentRemovalNotAuthorized = 310,
    AlreadyExists = 311,
    LikeRemovalNotAuthorized = 312,
    // User errors should be in the range 306-310
    UserDoesNotExist = 306,
    IncorrectPassword = 307,
    WrongPassword = 308,


    UnknownError = 999,
}