namespace BudgetManagementSystem.Infrastructure.CustomConfig;

public sealed class GoogleReCaptchaSetting
{
    public string SiteKey { get; set; }
    public string SecretKey { get; set; }
    public string VerifyUrl { get; set; }
    public string RecaptchaUrl { get; set; }
}

