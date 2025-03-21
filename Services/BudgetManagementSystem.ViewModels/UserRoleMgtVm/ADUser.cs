namespace BudgetManagementSystem.ViewModels.UserRoleMgtVm;

public sealed class ADUser
{
    public string FullName { get; set; }
    public string EmployeeId { get; set; }
    public string Title { get; set; }
    public string Mail { get; set; }
    public string UserName { get; set; }
    public string Phone { get; set; }
    public string OrganizationUnit { get; set; }
    public string Department { get; set; }
    public string LastName => FullName.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)[0] ?? "";
    public string FirstName => FullName.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)[1] ?? "";
    public string MiddleName => FullName.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Length > 2 ? FullName.Trim().Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)[2] : "";
}
