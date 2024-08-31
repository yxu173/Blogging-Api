using Domain.Enum;

namespace BloggingApi.Contracts.Post.Request;

public class LikeCreate
{
    public required InteractionType InteractionType { get; set; }
}