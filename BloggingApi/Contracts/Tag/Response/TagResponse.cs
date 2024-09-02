namespace BloggingApi.Contracts.Tag.Response;

public class TagResponse
{
    public required Guid Id { get; set; }
    public required string TagName { get; set; }
}