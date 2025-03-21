
using BudgetManagementSystem.ViewModels.UserRoleMgtVm;

namespace BudgetManagementSystem.BusinessLogic.Queries;

public record GetRolePermissionsQuery(string roleId) : IRequest<GetRolePermissionVm>;
public record GetPermissionsByRoleQuery(string roleId) : IRequest<List<PermissionVm>>;

