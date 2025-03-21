namespace BudgetManagementSystem.Infrastructure.CustomConfig;

public sealed class PasswordGenSetting
{
    public string MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS { get; set; }
    public string LOWERCASE_CHARACTERS { get; set; }
    public string UPPERCASE_CHARACTERS { get; set; }
    public string NUMERIC_CHARACTERS { get; set; }
    public string SPECIAL_CHARACTERS { get; set; }
    public string SPACE_CHARACTER { get; set; }
    public string PASSWORD_LENGTH_MIN { get; set; }
    public string PASSWORD_LENGTH_MAX { get; set; }
}
