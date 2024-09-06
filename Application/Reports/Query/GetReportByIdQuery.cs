using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Reports.Query;

public record GetReportByIdQuery(Guid ReportId) : IRequest<OperationResult<Report>>;