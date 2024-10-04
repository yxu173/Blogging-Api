using Application.Enums;
using Application.Identity.Commands;
using Application.Models;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Identity.CommandHandler;

public class VerificationEmailHandler(ApplicationDbContext _dbContext)
    : IRequestHandler<VerificationEmailCommand, OperationResult<bool>>
{
    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(
        VerificationEmailCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var user = await _dbContext
                .EmailVerificationTokens.Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Token, cancellationToken);

            if (user != null)
            {
                if (user.ExpirationDate > DateTime.UtcNow)
                {
                    user.User.EmailConfirmed = true;
                    _dbContext.EmailVerificationTokens.Remove(user);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    _result.Payload = true;
                }
                else
                {
                    _result.AddError(ErrorCode.TokenExpired, "Token expired");
                }
            }
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.UnknownError, e.Message);
            return _result;
        }

        return _result;
    }
}

