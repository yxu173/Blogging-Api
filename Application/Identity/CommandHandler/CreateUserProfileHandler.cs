using Application.Enums;
using Application.Identity.Commands;
using Application.Models;
using Application.Services;
using Domain.Entities;
using Infrastracture;
using MediatR;

namespace Application.Identity.CommandHandler;

public class CreateUserProfileHandler(ApplicationDbContext dbContext, UserServices userServices)
    : IRequestHandler<CreateUserProfileCommand, OperationResult<BasicInfo>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly UserServices _userServices = userServices;
    private OperationResult<BasicInfo> _result = new();

    public async Task<OperationResult<BasicInfo>> Handle(CreateUserProfileCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userServices.GetUserById(request.Id);
            if (user == null)
            {
                _result.AddError(ErrorCode.NotFound, "User Not Found");

                return _result;
            }

            var result = BasicInfo
                .CreateBasicInfo(request.ProfileImage,
                    request.Bio, request.SocialMediaLinks);

            user.AddBasicInfo(result);
            try
            {
                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync(cancellationToken);
                _result.Payload = result;
                return _result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return _result;
    }
}