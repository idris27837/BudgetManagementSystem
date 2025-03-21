namespace BudgetManagementSystem.ViewModels.AuthMgtVm;

public sealed class CookieData
{
    public string Id { get; set; } = "";
    public string JambRegNo { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; }
    public string Name { get; set; } = "";
    public string Token { get; set; } = "";
    public string Phone { get; set; } = "";
    public bool NeedPasswordReset { get; set; }
    public DateTime Expiry { get; set; }
    public string ReturnUrl { get; set; } = "/";

}

