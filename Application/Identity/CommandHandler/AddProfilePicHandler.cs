using Application.Enums;
using Application.Identity.Commands;
using Application.Models;
using Application.Services;
using Infrastracture;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Application.Identity.CommandHandler;

public class AddProfilePicHandler(UploadPhotoServices profilePicService)
    : IRequestHandler<AddProfilePicCommand, OperationResult<string>>
{
    private readonly UploadPhotoServices _profilePicService = profilePicService;


    public async Task<OperationResult<string>> Handle(AddProfilePicCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<string>();
        try
        {
            var response = await _profilePicService.UploadProfilePhoto(request.File, request.UserId);
            result.Payload = response;
        }
        catch (Exception e)
        {
            result.AddError(ErrorCode.UnknownError, e.Message);
        }

        return result;
    }
}