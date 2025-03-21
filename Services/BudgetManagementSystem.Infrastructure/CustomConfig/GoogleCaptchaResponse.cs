namespace BudgetManagementSystem.Infrastructure.CustomConfig;

public sealed class GoogleCaptchaResponse
{
    public bool Success { get; set; }
    public double Score { get; set; }
    public string Action { get; set; }
    public DateTime Challenge_ts { get; set; }
    public string Hostname { get; set; }
}

