using Domain.Enum;

namespace BloggingApi.Contracts.Post.Request;

public record LikeCreate(InteractionType InteractionType);