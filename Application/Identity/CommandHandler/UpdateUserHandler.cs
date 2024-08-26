using Application.Enums;
using Application.Identity.Commands;
using Application.Identity.DTOs;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Identity.CommandHandler;

public class UpdateUserHandler(UserServices userService, IMapper mapper)
    : IRequestHandler<UpdateUserCommand, OperationResult<UserUpdateDto>>
{
    private readonly UserServices _userService = userService;
    private IMapper _mapper = mapper;
    private OperationResult<UserUpdateDto> _result = new();

    public async Task<OperationResult<UserUpdateDto>> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _userService
                .UpdateUser(request.Id, request.UserName, request.Email);

            if (result == null)
            {
                _result.AddError(ErrorCode.NotFound, "User Not Found");

                return _result;
            }

            _result.Payload = _mapper.Map<UserUpdateDto>(result);
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