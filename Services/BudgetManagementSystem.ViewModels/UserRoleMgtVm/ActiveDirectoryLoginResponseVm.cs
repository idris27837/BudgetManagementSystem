namespace BudgetManagementSystem.ViewModels.UserRoleMgtVm;

public sealed class ActiveDirectoryLoginResponseVm
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public ADUser ADUser { get; set; }
}

public abstract class BaseAPIResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

}
