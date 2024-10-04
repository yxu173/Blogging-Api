using Application.Enums;
using Application.Models;
using Application.Reports.Query;
using Domain.Entities;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reports.QueryHandler;

public class GetReportByIdHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetReportByIdQuery, OperationResult<Report>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly OperationResult<Report> _result = new();

    public async Task<OperationResult<Report>> Handle(
        GetReportByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var report = await _dbContext
                .Reports.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.ReportId, cancellationToken);

            if (report == null)
            {
                _result.AddError(ErrorCode.NotFound, "Report Not Found");
                return _result;
            }

            _result.Payload = report;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.GetReportByIdFailed, e.Message);
            return _result;
        }

        return _result;
    }
}

