namespace BloggingApi.Contracts.Identity;

public record IdentityResponse(string UserName, string EmailAddress, string Token);