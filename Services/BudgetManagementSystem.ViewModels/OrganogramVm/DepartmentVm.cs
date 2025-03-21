namespace BudgetManagementSystem.ViewModels.OrganogramVm;

public sealed class DepartmentVm : BaseAuditVm
{
    public int DepartmentId { get; set; }
    public int? DirectorateId { get; set; }
    public string DepartmentName { get; set; }
    public string DepartmentCode { get; set; }
    public string DirectorateName { get; set; }
    public bool IsBranch { get; set; }

}
