using Application.Models;
using Application.Posts.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.PostTags.Query;

public record GetAllPostsByTagNameQuery(string TagName) : IRequest<OperationResult<List<PostDto>>>;