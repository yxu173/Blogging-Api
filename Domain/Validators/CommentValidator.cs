using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class CommentValidator : AbstractValidator<Comment>
{
    public CommentValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty();
    }
}