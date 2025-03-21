
using BudgetManagementSystem.Models.CompetencyMgt;

namespace BudgetManagementSystem.Models.Core;

public sealed class BankYear : BaseAudit
{
    public int BankYearId { get; set; }

    [UniqueKey(groupId: "1", order: 0)]
    [MaxLength(10)]
    public string YearName { get; set; }
    public ICollection<BudgetPeriod> BudgetPeriods { get; set; }
}
