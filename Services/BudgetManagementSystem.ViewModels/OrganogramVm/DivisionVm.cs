namespace BudgetManagementSystem.ViewModels.OrganogramVm;

public sealed class DivisionVm : BaseAuditVm
{
    public int DivisionId { get; set; }
    public int DepartmentId { get; set; }
    public string DivisionName { get; set; }
    public string DivisionCode { get; set; }
    public string DepartmentName { get; set; }
}
