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
        public const string UpdateProfile = "UpdateProfile";
        public const string GetUserProfile = "GetUserProfile";
    }

    public static class Post
    {
        public const string CreatePost = "CreatePost";
        public const string GetPostById = "GetPostById";
        public const string UpdatePost = "UpdatePost";
        public const string Delete = "Delete";
    }

    public static class Tag
    {
        public const string CreateTag = "CreateTag";
        public const string DeleteTag = "DeleteTag";
        public const string GetAllTags = "GetAllTags";
        public const string GetPostsByTagName = "GetPostsByTagName";
    }

    public static class Follow
    {
        public const string AddFollow = "AddFollow/{Id}";
        public const string RemoveFollow = "RemoveFollow";
        public const string GetAllFollowers = "GetAllFollowers";
        public const string GetAllFollowing = "GetAllFollowing";
    }
}