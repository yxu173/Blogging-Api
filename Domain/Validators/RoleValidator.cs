using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class RoleValidator : AbstractValidator<Role>
{
    public RoleValidator()
    {
        RuleFor(r => r.Name).NotEmpty();
    }
}