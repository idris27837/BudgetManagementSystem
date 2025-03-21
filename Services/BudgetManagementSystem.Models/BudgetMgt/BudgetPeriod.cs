namespace BudgetManagementSystem.Models.CompetencyMgt;

public sealed class BudgetPeriod : BaseWorkFlowData
{
    public int ReviewPeriodId { get; set; }
    public int BankYearId { get; set; }

    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public BankYear BankYear { get; set; }
}
