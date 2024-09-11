namespace Domain.Entities;

public class BasicInfo
{
    private BasicInfo()
    {
    }

    public string ProfileImage { get; private set; }
    public string Bio { get; private set; }
    public string SocialMediaLinks { get; private set; } // TODO: Assuming this is stored as a JSON string.

    public static BasicInfo CreateBasicInfo( string bio, string socialMediaLinks)
    {
        return new BasicInfo
        {
            Bio = bio,
            SocialMediaLinks = socialMediaLinks
        };
    }

    public void AddProfileImage(string imageUrl)
    {
        ProfileImage = imageUrl;
    }

}