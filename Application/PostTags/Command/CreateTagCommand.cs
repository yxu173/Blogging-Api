using Application.Models;
using Application.PostTags.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.PostTags.Command;

public class CreateTagCommand : IRequest<OperationResult<TagDto>>
{
    public required string TagName { get; set; }
}