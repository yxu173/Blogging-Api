using Application.Enums;
using Application.Identity.Query;
using Application.Models;
using Application.Services;
using Domain.Entities;
using MediatR;

namespace Application.Identity.QueryHandler;

public class GetUserByUserNameHandler(UserServices userServices)
    : IRequestHandler<GetUserByUserNameQuery, OperationResult<User>>
{
    private readonly UserServices _userServices = userServices;
    private OperationResult<User> _result = new();

    public async Task<OperationResult<User>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userServices.GetUserByUserName(request.UserName);
            if (user == null)
            {
                _result.AddError(ErrorCode.NotFound, "User not found");
                return _result;
            }

            _result.Payload = user;
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