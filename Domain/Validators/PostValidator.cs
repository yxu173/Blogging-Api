using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class PostValidator : AbstractValidator<Post>
{
    public PostValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();
        RuleFor(x => x.Content)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(3000);
    }
}