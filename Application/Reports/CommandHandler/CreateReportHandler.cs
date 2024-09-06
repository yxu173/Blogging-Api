using Application.Enums;
using Application.Models;
using Application.Reports.Command;
using Domain.Entities;
using Infrastracture;
using MediatR;

namespace Application.Reports.CommandHandler;

public class CreateReportHandler(ApplicationDbContext dbContext)
    : IRequestHandler<CreateReportCommand, OperationResult<Report>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly OperationResult<Report> _result = new();

    public async Task<OperationResult<Report>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var report = Report.Create(request.ContentId,
                request.ContentType, request.Reason);

            await _dbContext.Reports.AddAsync(report, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _result.Payload = report;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.ReportPostFailed, e.Message);
        }

        return _result;
    }
}