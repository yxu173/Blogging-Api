namespace BloggingApi.Contracts.Identity;

public record RegisterCreate(string UserName, string Email, string Password);