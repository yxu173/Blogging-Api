namespace Domain.Entities;

public class Follow
{
    private Follow()
    {
        CreatedAt = DateTime.Now.ToUniversalTime();
    }
    public Guid FollowerId { get; private set; }  // The user who is following another user(this user)
    public Guid FollowedId { get; private set; }  // The user who is being followed
    public DateTime CreatedAt { get; private set; }

    public User Follower { get; private set; }
    public User Followed { get; private set; }

    public static Follow CreateFollow(Guid followerId, Guid followedId)
    {
        return new Follow
        {
            FollowerId = followerId,
            FollowedId = followedId
        };
    }
}