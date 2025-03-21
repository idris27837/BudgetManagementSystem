using Humanizer;

namespace BudgetManagementSystem.ViewModels.UserRoleMgtVm;

public sealed class CurrentUserData
{
    public string Phone { get; set; }
    public string UserId { get; set; }
    public string JambRegNo { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public List<string> Roles { get; set; }
    public string Token { get; set; }
    public bool NeedPasswordReset { get; set; }
    public string Photo { get; set; } = "assets/media/avater.png";
    public string MajorRole => Roles.FirstOrDefault().Humanize(LetterCasing.AllCaps);
}
