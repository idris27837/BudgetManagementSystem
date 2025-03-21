namespace BudgetManagementSystem.Infrastructure.Abstractions;

public interface IDateTimeService
{
    DateTime NowUtc { get; }
}
