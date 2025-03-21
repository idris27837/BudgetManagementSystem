using BudgetManagementSystem.Models.Core;
using BudgetManagementSystem.ViewModels.StaffMgtVm;

namespace BudgetManagementSystem.BusinessLogic.Commands;

public record SaveRoleCmd(RoleVm Vm) : IRequest<ResponseVm>;
public record DeleteRoleCmd(string RoleName) : IRequest<ResponseVm>;
public record CreateStaffCmd(ApplicationUser Vm) : IRequest<ResponseVm>;
public record AddStaffToRoleCmd(AddStaffToRoleVm Vm) : IRequest<ResponseVm>;
public record DeleteStaffFromRoleCmd(string UserId, string RoleName) : IRequest<ResponseVm>;

