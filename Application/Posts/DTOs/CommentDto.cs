using Domain.Entities;

namespace Application.Posts.DTOs;

public record CommentDto(Guid Id, Guid PostId, Guid UserId, string? UserName,string Content, DateTime CreatedAt);
// {
//     public Guid Id { get; set; }
//     public Guid PostId { get; set; }
//     public Guid UserId { get; set; }
//     public string? UserName { get; set; }
//     public string Content { get; set; }
//     public DateTime CreatedAt { get; set; }
//
//
//     public static CommentDto CreateCommentDto(Comment comment)
//     {
//         return new CommentDto
//         {
//             Id = comment.Id,
//             PostId = comment.PostId,
//             UserId = comment.UserId,
//             Content = comment.Content,
//             CreatedAt = comment.CreatedAt
//         };
//     }
// }