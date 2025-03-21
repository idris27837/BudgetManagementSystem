
using BudgetManagementSystem.ViewModels.UserRoleMgtVm;

namespace BudgetManagementSystem.Infrastructure.Abstractions;

public interface IUserDbContext
{
    string UserId { get; }
    string Email { get; }
    List<KeyValuePair<string, string>> Claims { get; }
    bool IsAuthenticated();
    bool IsInRole(string role);
    CurrentUserData CurrentUser { get; }
    List<string> GetUserRoles();

}