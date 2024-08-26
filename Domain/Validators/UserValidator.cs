using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .NotNull();
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}