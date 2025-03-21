using System.Linq.Expressions;

namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetOfficeJobRolesQueryHandler(IRepo<OfficeJobRole> repo) : IRequestHandler<GetOfficeJobRolesQuery, OfficeJobRoleListVm>
{
    public async ValueTask<OfficeJobRoleListVm> Handle(GetOfficeJobRolesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<OfficeJobRole> query;
        var filters = new List<Expression<Func<OfficeJobRole, bool>>>();

        if (request.Vm.OfficeId.HasValue)
        {
            filters.Add(y => y.OfficeId.Equals((int)request.Vm.OfficeId));
        }

        if (!string.IsNullOrWhiteSpace(request.Vm.SearchString))
        {
            filters.Add(y => y.JobRole.JobRoleName.Contains(request.Vm.SearchString)
                        || y.Office.OfficeName.Contains(request.Vm.SearchString));
        }

        query = repo.GetAllByQueriesPagination(request.Vm.Skip, request.Vm.PageSize, filters).Include(i => i.Office).Include(i => i.JobRole);

        var totalRecords = await repo.CountFiltersAsync(filters);

        var models = await query.Select(s => new OfficeJobRoleVm
        {
            OfficeJobRoleId = s.OfficeJobRoleId,
            OfficeId = s.OfficeId,
            OfficeName = s.Office.OfficeName,
            JobRoleId = s.JobRoleId,
            JobRoleName = s.JobRole.JobRoleName,
            IsActive = s.IsActive
        }).ToListAsync(cancellationToken);

        return new OfficeJobRoleListVm
        {
            OfficeJobRoles = models,
            TotalRecord = totalRecords
        };
    }
}

public class SaveOfficeJobRoleHandler(IRepo<OfficeJobRole> repo) : IRequestHandler<SaveOfficeJobRoleCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(SaveOfficeJobRoleCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.OfficeJobRoleId > 0)
        {
            var jobRole = await repo.GetById(request.Vm.OfficeJobRoleId);
            if (jobRole != null)
            {
                jobRole.OfficeId = request.Vm.OfficeId;
                jobRole.JobRoleId = request.Vm.JobRoleId;
                jobRole.IsActive = request.Vm.IsActive;

                repo.UpdateRecord(jobRole);
                message = "Office Job Role has been updated successfully";
            }
        }
        else
        {
            var model = new OfficeJobRole
            {
                OfficeId = request.Vm.OfficeId,
                JobRoleId = request.Vm.JobRoleId,
                IsActive = request.Vm.IsActive
            };
            await repo.AddRecord(model);
            message = "Office Job Role has been created successfully";
        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteOfficeJobRoleHandler(IRepo<OfficeJobRole> repo) : IRequestHandler<DeleteOfficeJobRoleCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(DeleteOfficeJobRoleCmd request, CancellationToken cancellationToken)
    {
        var jobRole = await repo.GetById(request.Id);

        if (request.IsSoftDelete && jobRole != null)
        {
            jobRole.SoftDeleted = true;
            jobRole.IsActive = false;
            repo.UpdateRecord(jobRole);
        }
        else
        {
            await repo.Delete(request.Id);
        }

        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Job Role has been deleted successfully" : result.Message };
    }
}
