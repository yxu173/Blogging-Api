namespace BloggingApi.Contracts.Identity;

public class IdentityResponse
{
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string Token { get; set; }
}