using Application.Models;
using MediatR;

namespace Application.Reports.Command;

public record ResolveReportCommand(Guid ReportId) : IRequest<OperationResult<bool>>;