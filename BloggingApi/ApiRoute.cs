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
        public const string UpdateUser = "UpdateUser";
    }

    public static class Post
    {
        public const string CreatePost = "CreatePost";
        public const string Get = "Get";
        public const string Update = "Update";
        public const string Delete = "Delete";
    }
}