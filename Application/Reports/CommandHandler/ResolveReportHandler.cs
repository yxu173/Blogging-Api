using Application.Enums;
using Application.Models;
using Application.Reports.Command;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reports.CommandHandler;

public class ResolveReportHandler(ApplicationDbContext dbContext)
    : IRequestHandler<ResolveReportCommand, OperationResult<bool>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly OperationResult<bool> _result = new();

    public async Task<OperationResult<bool>> Handle(ResolveReportCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var report = await _dbContext
                .Reports
                .FirstOrDefaultAsync(x => x.Id == request.ReportId
                    , cancellationToken);

            if (report == null)
            {
                _result.AddError(ErrorCode.NotFound, "Report Not Found");
                return _result;
            }

            report.Resolve();
            _dbContext.Reports.Update(report);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = true;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.ResolveReportFailed, e.Message);
        }

        return _result;
    }
}