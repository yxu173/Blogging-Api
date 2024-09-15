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
    TokenExpired = 310,
    
    //Tag errors should be in the range 300 - 399
    TagCreationFailed = 310,
    TagDeletionFailed = 311,
    TagUpdateFailed = 312,
    TagDoesNotExist = 313,
    
    //Post errors should be in the range 300 - 399
    PostCreationFailed = 320,
    PostDeletionFailed = 321,
    PostUpdateFailed = 322,
    
    
    //Comment errors should be in the range 300 - 399
    CommentCreationFailed = 330,
    CommentDeletionFailed = 331,
    CommentUpdateFailed = 332,
    CommentRetrievalFailed = 333,
    
    
    //Like errors should be in the range 300 - 399
    LikeCreationFailed = 340,
    LikeDeletionFailed = 341,
    LikeUpdateFailed = 342,
    LikeRetrievalFailed = 343,
    
    
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
    PostRetrievalFailed = 304,
    CommentNotAuthorized = 305,
    CommentDoesNotExist = 306,
    

//Application errors should be in the range 300 - 399
    CommentRemovalNotAuthorized = 310,
    AlreadyExists = 311,
    LikeRemovalNotAuthorized = 312,
    
    //Report errors should be in the range 300 - 399
    ReportCreationFailed = 320,
    GetReportByIdFailed = 321,
    GetAllReportsFailed = 322,
    ResolveReportFailed = 323,
    ReportPostFailed = 324,
    
    // User errors should be in the range 306-310
    UserDoesNotExist = 306,
    IncorrectPassword = 307,
    WrongPassword = 308,


    UnknownError = 999,
}