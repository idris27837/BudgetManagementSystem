namespace BudgetManagementSystem.ViewModels.StaffMgtVm;

public sealed class AddStaffToRoleVm
{
    [Required]
    public string UserId { get; set; }
    public string RoleId { get; set; }
    [Required]
    public string RoleName { get; set; }
    public string StaffName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
