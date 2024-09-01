using Application.Enums;
using Application.Identity.DTOs;
using Application.Identity.Query;
using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Identity.QueryHandler;

public class GetUserByIdHandler(UserServices userService, IMapper mapper)
    : IRequestHandler<GetUserByIdQuery, OperationResult<IdentityUserDto>>
{
    private readonly UserServices _userService = userService;
    private IMapper _mapper = mapper;
    private OperationResult<IdentityUserDto> _result = new();

    public async Task<OperationResult<IdentityUserDto>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserById(request.Id);
        if (user == null)
        {
            _result.AddError(ErrorCode.NotFound, "User Not Found");
        }

        _result.Payload = _mapper.Map<IdentityUserDto>(user);

        return _result;
    }
}