namespace BudgetManagementSystem.ViewModels.StaffMgtVm;

public sealed class RoleVm
{
    public string Id { get; set; }

    [Required]
    [RegularExpression(@"[a-zA-Z_\-]+$", ErrorMessage = "Role name can contain underscore, hyphen, letters and no spaces.")]
    public string RoleName { get; set; }
}

