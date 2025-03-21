
using BudgetManagementSystem.ViewModels.UserRoleMgtVm;

namespace BudgetManagementSystem.BusinessLogic.Commands
{
    public record AddPermissionToRoleCmd(AddPermissionToRoleVm Vm) : IRequest<ResponseVm>;
    public record RemovePermissionFromRoleCmd(string roleId, int permissionId) : IRequest<ResponseVm>;
}
