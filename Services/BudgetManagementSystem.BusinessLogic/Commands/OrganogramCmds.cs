using BudgetManagementSystem.ViewModels.OrganogramVm;

namespace BudgetManagementSystem.BusinessLogic.Commands;


public record SaveDepartmentCmd(DepartmentVm Vm) : IRequest<ResponseVm>;
public record DeleteDepartmentCmd(int Id, bool IsSoftDelete = false) : IRequest<ResponseVm>;


public record SaveDirectorateCmd(DirectorateVm Vm) : IRequest<ResponseVm>;
public record DeleteDirectorateCmd(int Id, bool IsSoftDelete = false) : IRequest<ResponseVm>;


public record SaveDivisionCmd(DivisionVm Vm) : IRequest<ResponseVm>;
public record DeleteDivisionCmd(int Id, bool IsSoftDelete = false) : IRequest<ResponseVm>;


public record SaveOfficeCmd(OfficeVm Vm) : IRequest<ResponseVm>;
public record DeleteOfficeCmd(int Id, bool IsSoftDelete = false) : IRequest<ResponseVm>;



