using BudgetManagementSystem.ViewModels.OrganogramVm;

namespace BudgetManagementSystem.BusinessLogic.Queries;

public record GetDirectorateQuery() : IRequest<List<DirectorateVm>>;
public record GetDepartmentQuery(int? DirectorateId = null) : IRequest<List<DepartmentVm>>;
public record GetDivisionQuery(int? DepartmentId = null) : IRequest<List<DivisionVm>>;
public record GetOfficeQuery(int? DivisionId = null) : IRequest<List<OfficeVm>>;
public record GetOfficeByIdQuery(string OfficeId) : IRequest<OfficeVm>;
