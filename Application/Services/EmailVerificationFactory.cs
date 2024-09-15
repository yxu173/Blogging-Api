using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Services;

public sealed class EmailVerificationFactory(
    IHttpContextAccessor accessor,
    LinkGenerator linkGenerator)
{
    public string GenerateEmailVerificationLink(EmailVerificationToken verificationToken)
    {
        var context = accessor.HttpContext;
        var routeValues = new { verificationToken.Id };
        var link = linkGenerator.GetUriByAction(context,
            action: "VerifyEmail",
            controller: "Identity",
            values: routeValues);
        return link ?? throw new Exception("Could not generate email verification link");
    }
}