namespace BudgetManagementSystem.ViewModels.OrganogramVm;

public sealed class OfficeVm : BaseAuditVm
{
    public int OfficeId { get; set; }
    public int DivisionId { get; set; }
    public string OfficeName { get; set; }
    public string OfficeCode { get; set; }
    public string DivisionName { get; set; }
}
