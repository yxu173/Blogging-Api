using Domain.Entities;

namespace Application.Identity.DTOs;

public record BasicInfoDto(
    string Bio,
    string ProfileImage,
    string SocialLinks);