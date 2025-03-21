using System.Linq.Expressions;

namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetJobRoleCompetenciesQueryHandler : IRequestHandler<GetJobRoleCompetenciesQuery, PagedJobRoleCompetencyVm>
{
    private readonly IRepo<JobRoleCompetency> _repo;

    public GetJobRoleCompetenciesQueryHandler(IRepo<JobRoleCompetency> repo)
    {
        _repo = repo;
    }

    public async ValueTask<PagedJobRoleCompetencyVm> Handle(GetJobRoleCompetenciesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<JobRoleCompetency> query;
        List<Expression<Func<JobRoleCompetency, bool>>> filters = new();

        if (request.Vm.OfficeId.HasValue)
        {
            filters.Add(y => y.OfficeId.Equals((int)request.Vm.OfficeId));
        }
        else if (request.Vm.DivisionId.HasValue)
        {
            filters.Add(y => y.Office.DivisionId.Equals((int)request.Vm.DivisionId));
        }
        else if (request.Vm.DepartmentId.HasValue)
        {
            filters.Add(y => y.Office.Division.DepartmentId.Equals((int)request.Vm.DepartmentId));
        }

        if (request.Vm.JobRoleId.HasValue)
        {
            filters.Add(y => y.JobRoleId.Equals((int)request.Vm.JobRoleId));
        }

        if (!string.IsNullOrWhiteSpace(request.Vm.SearchString))
        {
            filters.Add(y => y.Competency.CompetencyName.Contains(request.Vm.SearchString.ToUpper()));
        }

        query = _repo.GetAllByQueriesPagination(request.Vm.Skip, request.Vm.PageSize, filters).Include(i => i.Competency).Include(i => i.JobRole)
                         .Include(i => i.Office.Division.Department).Include(i => i.Rating);

        var totalRecords = await _repo.CountFiltersAsync(filters);

        var model = await query.Select(s => new JobRoleCompetencyVm
        {
            JobRoleCompetencyId = s.JobRoleCompetencyId,
            CompetencyId = s.CompetencyId,
            CompetencyName = s.Competency.CompetencyName,
            JobRoleId = s.JobRoleId,
            JobRoleName = s.JobRole.JobRoleName ?? "",
            OfficeId = s.OfficeId,
            OfficeName = s.Office.OfficeName ?? "All",
            RatingId = s.RatingId,
            RatingName = s.Rating.Name,
            DepartmentName = s.Office.Division.Department.DepartmentName ?? "All",
            DepartmentId = s.Office.Division.DepartmentId,
            DivisionId = s.Office.DivisionId,
            DivisionName = s.Office.Division.DivisionName ?? "All",
            IsActive = s.IsActive
        }).ToListAsync();

        return new PagedJobRoleCompetencyVm
        {
            JobRoleCompetencies = model,
            TotalRecords = totalRecords
        };
    }
}


public class SaveJobRoleCompetencyHandler : IRequestHandler<SaveJobRoleCompetencyCmd, ResponseVm>
{
    private readonly IRepo<JobRoleCompetency> _repo;

    public SaveJobRoleCompetencyHandler(IRepo<JobRoleCompetency> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(SaveJobRoleCompetencyCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.JobRoleCompetencyId > 0)
        {
            var jobRoleCompetency = await _repo.GetById(request.Vm.JobRoleCompetencyId);
            if (jobRoleCompetency != null)
            {
                jobRoleCompetency.JobRoleCompetencyId = request.Vm.JobRoleCompetencyId;
                jobRoleCompetency.CompetencyId = request.Vm.CompetencyId;
                jobRoleCompetency.JobRoleId = request.Vm.JobRoleId;
                jobRoleCompetency.OfficeId = request.Vm.OfficeId;
                jobRoleCompetency.RatingId = request.Vm.RatingId;
                jobRoleCompetency.IsActive = request.Vm.IsActive;

                _repo.UpdateRecord(jobRoleCompetency);
                message = $"{request.Vm.CompetencyName} has been added to the {request.Vm.JobRoleName} successfully";
            }
        }
        else
        {
            var model = new JobRoleCompetency
            {
                JobRoleCompetencyId = request.Vm.JobRoleCompetencyId,
                CompetencyId = request.Vm.CompetencyId,
                JobRoleId = request.Vm.JobRoleId,
                OfficeId = request.Vm.OfficeId,
                RatingId = request.Vm.RatingId,
                IsActive = request.Vm.IsActive
            };
            await _repo.AddRecord(model);
            message = "Added successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : $"{request.Vm.CompetencyName} already exist for {request.Vm.JobRoleName}" };
    }
}

public class DeleteJobRoleCompetencyHandler : IRequestHandler<DeleteJobRoleCompetencyCmd, ResponseVm>
{
    private readonly IRepo<JobRoleCompetency> _repo;

    public DeleteJobRoleCompetencyHandler(IRepo<JobRoleCompetency> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteJobRoleCompetencyCmd request, CancellationToken cancellationToken)
    {
        var jobRoleCompetency = await _repo.GetById(request.Id);

        if (request.IsSoftDelete && jobRoleCompetency != null)
        {
            jobRoleCompetency.SoftDeleted = true;
            jobRoleCompetency.IsActive = false;
            _repo.UpdateRecord(jobRoleCompetency);
        }
        else
        {
            await _repo.Delete(request.Id);
        }

        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Job Role Competency has been deleted successfully" : result.Message };
    }
}

