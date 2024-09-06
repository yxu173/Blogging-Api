namespace BloggingApi.Contracts.Identity;

public record IdentityResponse(string UserName, string Email, string Token);