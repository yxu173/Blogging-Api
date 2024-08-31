using Domain.Entities;

namespace Application.Identity.DTOs;

public class BasicInfoDto
{
    public string Bio { get; set; }
    public string ProfileImage { get; set; }
    public string SocialLinks { get; set; }

    public static BasicInfoDto CreateBasicInfo(BasicInfo info)
    {
        return new BasicInfoDto
        {
            Bio = info.Bio,
            ProfileImage = info.ProfileImage,
            SocialLinks = info.SocialMediaLinks
        };
    }
}