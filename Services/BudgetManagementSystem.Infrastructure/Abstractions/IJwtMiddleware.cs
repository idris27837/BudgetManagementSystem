// Ignore Spelling: Jwt

using BudgetManagementSystem.ViewModels.AuthMgtVm;
using BudgetManagementSystem.ViewModels.UserRoleMgtVm;

namespace BudgetManagementSystem.Infrastructure.Abstractions;

public interface IJwtMiddleware
{
    public TokenResponse GenerateToken(ADUser user, IList<string> userRoles, int tokenExpiryMinutes);
    public string ValidateToken(string token);
    TokenResponse GenerateAccessToken(ADUser user, IList<string> userRoles, int tokenExpiryMinutes);
    string GenerateRefreshToken();
}
