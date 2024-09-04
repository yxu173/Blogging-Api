using Application.Models;
using Application.PostTags.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.PostTags.Query;

public record GetAllTagsQuery : IRequest<OperationResult<IReadOnlyList<TagDto>>>;