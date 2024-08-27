namespace BloggingApi.Contracts.Identity;

public class ProfileDto
{
    public required string ProfileImage { get; set; }
    public required string Bio { get; set; }
    public required string SocialMediaLinks { get; set; }
}