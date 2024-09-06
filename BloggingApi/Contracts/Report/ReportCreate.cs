using Domain.Enum;

namespace BloggingApi.Contracts.Report;

public record ReportCreate(Guid Id, Reason Reason);