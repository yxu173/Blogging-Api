namespace BloggingApi.Contracts.Post.Request;

public record PostCreate(string Title, string Content, List<IFormFile> Images);
