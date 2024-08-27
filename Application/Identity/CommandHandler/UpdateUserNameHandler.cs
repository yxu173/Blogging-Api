using Application.Enums;
using Application.Identity.Commands;
using Application.Identity.DTOs;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Identity.CommandHandler;

public class UpdateUserNameHandler(UserServices userService, IMapper mapper)
    : IRequestHandler<UpdateUserNameCommand, OperationResult<UsernameUpdateDto>>
{
    private readonly UserServices _userService = userService;
    private IMapper _mapper = mapper;
    private OperationResult<UsernameUpdateDto> _result = new();

    public async Task<OperationResult<UsernameUpdateDto>> Handle(UpdateUserNameCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _userService
                .UpdateUserName(request.Id, request.UserName);

            if (result == null)
            {
                _result.AddError(ErrorCode.NotFound, "User Not Found");

                return _result;
            }

            _result.Payload = _mapper.Map<UsernameUpdateDto>(result);
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