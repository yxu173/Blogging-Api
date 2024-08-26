using Application.Identity.Commands;
using Application.Models;
using Application.Services;
using MediatR;

namespace Application.Identity.CommandHandler;

public class DeleteUserHandler(UserServices userService) : IRequestHandler<DeleteUserCommand, OperationResult<bool>>
{
    private readonly UserServices _userService = userService;
    private OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _userService.DeleteUserById(request.Id);
            _result.Payload = result;
            return _result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return _result;
    }
}