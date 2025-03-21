namespace BudgetManagementSystem.Models.Core;

public class RolePermission : BaseAudit
{
    public int RolePermissionId { get; set; }
    public int PermissionId { get; set; }
    public string RoleId { get; set; }
    public ApplicationRole Role { get; set; }
    public Permission Permission { get; set; }
}
