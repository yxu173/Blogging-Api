using Application.Models;
using Domain.Entities;
using MediatR;

namespace Application.Reports.Query;

public record GetAllReportsQuery() : IRequest<OperationResult<IReadOnlyList<Report>>>;