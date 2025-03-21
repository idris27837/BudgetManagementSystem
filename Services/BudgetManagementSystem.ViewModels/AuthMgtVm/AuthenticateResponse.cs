using BudgetManagementSystem.ViewModels.UserRoleMgtVm;

namespace BudgetManagementSystem.ViewModels.AuthMgtVm;

public sealed class AuthenticateResponse : BaseAPIResponse
{
    public string UserId { get; set; } = "";
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Token { get; set; }
    public string Phone { get; set; }
    public bool NeedPasswordReset { get; set; }
    public DateTime ExpiryDate { get; set; }

    public string FullName => $"{LastName} {FirstName}";
    public List<string> RoleNames { get; set; }
}

public class TokenResponse
{
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }

}

