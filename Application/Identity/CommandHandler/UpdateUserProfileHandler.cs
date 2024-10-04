using Application.Enums;
using Application.Exceptions.IdentityExceptions;
using Application.Identity.Commands;
using Application.Models;
using Application.Services;
using Domain.Entities;
using Infrastracture;
using MediatR;

namespace Application.Identity.CommandHandler;

public class UpdateUserProfileHandler(ApplicationDbContext dbContext, UserServices userServices)
    : IRequestHandler<UpdateUserProfileCommand, OperationResult<BasicInfo>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly UserServices _userServices = userServices;
    private OperationResult<BasicInfo> _result = new();

    public async Task<OperationResult<BasicInfo>> Handle(
        UpdateUserProfileCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var user = await _userServices.GetUserById(request.Id);
            if (user == null)
            {
                _result.AddError(ErrorCode.NotFound, "User Not Found");
                return _result;
            }

            var result = BasicInfo.CreateBasicInfo(request.Bio, request.SocialMediaLinks);
            user.UpdateBasicInfo(result);
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = result;
        }
        catch (UpdateUserProfileEx e)
        {
            _result.AddError(ErrorCode.UpdateUserProfileFailed, e.Message);
            return _result;
        }

        return _result;
    }
}

