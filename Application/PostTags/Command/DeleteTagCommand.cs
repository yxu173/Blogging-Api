using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.PostTags.Command;

public record DeleteTagCommand(string TagName) : IRequest<OperationResult<bool>>;