using BudgetManagementSystem.Infrastructure.Abstractions;

namespace BudgetManagementSystem.Infrastructure.Concrete;

public class DateTimeService : IDateTimeService
{
    public DateTime NowUtc => DateTime.UtcNow;
}
