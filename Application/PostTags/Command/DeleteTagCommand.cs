using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.PostTags.Command;

public class DeleteTagCommand : IRequest<OperationResult<bool>>
{
    public required string TagName { get; set; }
}