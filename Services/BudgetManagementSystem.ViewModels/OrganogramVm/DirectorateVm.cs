namespace BudgetManagementSystem.ViewModels.OrganogramVm;

public sealed class DirectorateVm : BaseAuditVm
{
    public int DirectorateId { get; set; }

    [Display(Name = "Directorate Name")]
    public string DirectorateName { get; set; }

    [Display(Name = "Directorate Code")]
    public string DirectorateCode { get; set; }
}
