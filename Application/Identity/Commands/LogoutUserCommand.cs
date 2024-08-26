using Application.Models;
using MediatR;

namespace Application.Identity.Commands;

public class LogoutUserCommand : IRequest<OperationResult<bool>>
{
    
}