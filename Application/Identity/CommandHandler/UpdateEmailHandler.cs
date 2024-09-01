using Application.Enums;
using Application.Exceptions.IdentityExceptions;
using Application.Identity.Commands;
using Application.Identity.DTOs;
using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;

namespace Application.Identity.CommandHandler;

public class UpdateEmailHandler(UserServices userService, IMapper mapper)
    : IRequestHandler<UpdateEmailCommand, OperationResult<EmailUpdateDto>>
{
    private readonly UserServices _userService = userService;
    private readonly IMapper _mapper = mapper;
    private OperationResult<EmailUpdateDto> _result = new();

    public async Task<OperationResult<EmailUpdateDto>> Handle(UpdateEmailCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _userService
                .UpdateEmail(request.Id, request.EmailAddress);
            if (result == null)
            {
                _result.AddError(ErrorCode.NotFound, "User Not Found");

                return _result;
            }

            _result.Payload = _mapper.Map<EmailUpdateDto>(result);
        }
        catch (UpdateEmailEx e)
        {
            _result.AddError(ErrorCode.UpdateEmailFailed, e.Message);
        }

        return _result;
    }
}