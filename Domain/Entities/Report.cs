using Domain.Common;
using Domain.Enum;
using Domain.Exceptions;
using Domain.Validators;

namespace Domain.Entities;

public class Report : BaseAuditableEntity
{
    private Report()
    {
    }

    public Guid ContentId { get; private init; } // The ID of the post or comment being reported
    public ContentType ContentType { get; private init; } // "Post" or "Comment"
    public Reason Reason { get; private init; }
    public bool IsResolved { get; private set; }

    public static Report Create(Guid contentId, ContentType contentType, Reason reason)
    {
        var report = new Report
        {
            Id = Guid.NewGuid(),
            ContentId = contentId,
            ContentType = contentType,
            Reason = reason,
            IsResolved = false,
            CreatedAt = DateTime.UtcNow.ToUniversalTime()
        };

        // var validator = new ReportValidator();
        // var validationResult = validator.Validate(report);
        //
        // if (!validationResult.IsValid)
        // {
        //     var exception = new ReportException("Invalid report");
        //     validationResult.Errors
        //         .ToList()
        //         .ForEach(error => exception.ValidationErrors.Add(error.ErrorMessage));
        //     throw exception;
        // }

        return report;
    }

    public void Resolve()
    {
        IsResolved = true;
    }
}