namespace BudgetManagementSystem.ViewModels.AuthMgtVm;

public sealed class AuthenticateRequest
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }

    public bool RememberMe { get; set; }
    public string IPAddress { get; set; } = "";
    public string DeviceName { get; set; } = "";
}

public sealed class RefreshTokenRequest
{
    public string Token { get; set; }
}
