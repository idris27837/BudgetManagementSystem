namespace BudgetManagementSystem.ViewModels.AuthMgtVm;

public class ForgotPasswordRequest
{
    [Required]
    public string Email { get; set; }
    public string ClientHost { get; set; }
}
