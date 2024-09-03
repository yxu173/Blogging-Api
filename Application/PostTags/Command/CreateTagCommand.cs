using Application.Models;
using Application.PostTags.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.PostTags.Command;

public record CreateTagCommand(string TagName) : IRequest<OperationResult<TagDto>>;