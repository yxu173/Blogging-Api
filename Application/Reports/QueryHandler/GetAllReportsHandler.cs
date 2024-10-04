using Application.Enums;
using Application.Models;
using Application.Reports.Query;
using Domain.Entities;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Reports.QueryHandler;

public class GetAllReportsHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetAllReportsQuery, OperationResult<IReadOnlyList<Report>>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly OperationResult<IReadOnlyList<Report>> _result = new();

    public async Task<OperationResult<IReadOnlyList<Report>>> Handle(
        GetAllReportsQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var reports = await _dbContext.Reports.AsNoTracking().ToListAsync(cancellationToken);

            if (reports.Count == 0)
            {
                _result.AddError(ErrorCode.NotFound, "There are no reports");
                return _result;
            }
            _result.Payload = reports;
        }
        catch (Exception e)
        {
            _result.AddError(ErrorCode.GetAllReportsFailed, e.Message);

            return _result;
        }

        return _result;
    }
}

