using Domain.Entities;
using FluentValidation;

namespace Domain.Validators;

public class ReportValidator : AbstractValidator<Report>
{
    public ReportValidator()
    {
        RuleFor(x => x.ContentId)
            .NotEmpty();
        RuleFor(x => x.ContentType).IsInEnum()
            .WithMessage("Invalid content type")
            .NotEmpty();
        RuleFor(x => x.Reason).IsInEnum()
            .WithMessage("Invalid reason")
            .NotEmpty();
    }
}