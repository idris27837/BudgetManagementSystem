namespace BudgetManagementSystem.Infrastructure.Abstractions;

public interface IPasswordGenerator
{
    string GeneratePassword(bool includeLowercase, bool includeUppercase, bool includeNumeric, bool includeSpecial, bool includeSpaces, int lengthOfPassword);
    bool IsValid();
}
