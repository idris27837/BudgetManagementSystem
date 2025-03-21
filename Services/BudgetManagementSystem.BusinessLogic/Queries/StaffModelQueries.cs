using BudgetManagementSystem.Models.Core;
using BudgetManagementSystem.ViewModels.StaffMgtVm;

namespace BudgetManagementSystem.BusinessLogic.Queries;

public record GetRoleQuery() : IRequest<List<RoleVm>>;
public record GetStaffRoleQuery(string StaffId) : IRequest<List<string>>;
public record GetStaffsQuery(string searchSting) : IRequest<List<ApplicationUser>>;