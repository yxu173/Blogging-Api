namespace BloggingApi;

public static class ApiRoute
{
    public const string BaseRoute = "api/[controller]";

    public static class User
    {
        public const string Register = "Register";
        public const string Login = "Login";
        public const string Logout = "Logout";
        public const string DeleteUser = "DeleteUser";
        public const string GetByUserName = "GetByUserName";
        public const string GetById = "GetById";
        public const string UpdateUsername = "UpdateUsername";
        public const string UpdateEmail = "UpdateEmail";
        public const string CreateProfile = "CreateProfile";
        public const string UpdateProfile = "UpdateProfile";
    }

    public static class Post
    {
        public const string CreatePost = "CreatePost";
        public const string GetPostById = "GetPostById";
        public const string UpdatePost = "UpdatePost";
        public const string Delete = "Delete";
    }
}