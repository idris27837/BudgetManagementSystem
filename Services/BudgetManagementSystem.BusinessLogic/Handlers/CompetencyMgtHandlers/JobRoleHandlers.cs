namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;
public class GetJobRolesQueryHandler : IRequestHandler<GetJobRolesQuery, List<JobRoleVm>>
{
    private readonly IRepo<JobRole> _repo;

    public GetJobRolesQueryHandler(IRepo<JobRole> repo)
    {
        _repo = repo;
    }

    public async ValueTask<List<JobRoleVm>> Handle(GetJobRolesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<JobRole> query;

        query = _repo.GetAllByQuery(null);

        return await query.Select(s => new JobRoleVm
        {
            JobRoleId = s.JobRoleId,
            JobRoleName = s.JobRoleName,
            Description = s.Description,
            IsActive = s.IsActive
        }).ToListAsync();
    }
}

public class GetJobRoleByNameQueryHandler : IRequestHandler<GetJobRoleByNameQuery, JobRoleVm>
{
    private readonly IRepo<JobRole> _repo;

    public GetJobRoleByNameQueryHandler(IRepo<JobRole> repo)
    {
        _repo = repo;
    }

    public async ValueTask<JobRoleVm> Handle(GetJobRoleByNameQuery request, CancellationToken cancellationToken)
    {
        IQueryable<JobRole> query;

        query = _repo.GetAllByQuery(x => x.JobRoleName.ToLower().Equals(request.JobRoleName.ToLower()));

        return await query.Select(s => new JobRoleVm
        {
            JobRoleId = s.JobRoleId,
            JobRoleName = s.JobRoleName,
            Description = s.Description,
            IsActive = s.IsActive
        }).FirstOrDefaultAsync();
    }
}

public class SaveJobRoleHandler : IRequestHandler<SaveJobRoleCmd, ResponseVm>
{
    private readonly IRepo<JobRole> _repo;

    public SaveJobRoleHandler(IRepo<JobRole> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(SaveJobRoleCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.JobRoleId > 0)
        {
            var jobRole = await _repo.GetById(request.Vm.JobRoleId);
            if (jobRole != null)
            {
                jobRole.JobRoleName = request.Vm.JobRoleName;
                jobRole.Description = request.Vm.Description;
                jobRole.IsActive = request.Vm.IsActive;

                _repo.UpdateRecord(jobRole);
                message = "Job Role has been updated successfully";
            }
        }
        else
        {
            var model = new JobRole
            {
                JobRoleName = request.Vm.JobRoleName,
                Description = request.Vm.Description,
                IsActive = request.Vm.IsActive
            };
            await _repo.AddRecord(model);
            message = "Job Role has been created successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteJobRoleHandler : IRequestHandler<DeleteJobRoleCmd, ResponseVm>
{
    private readonly IRepo<JobRole> _repo;

    public DeleteJobRoleHandler(IRepo<JobRole> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteJobRoleCmd request, CancellationToken cancellationToken)
    {
        var jobRole = await _repo.GetById(request.Id);

        if (request.IsSoftDelete && jobRole != null)
        {
            jobRole.SoftDeleted = true;
            jobRole.IsActive = false;
            _repo.UpdateRecord(jobRole);
        }
        else
        {
            await _repo.Delete(request.Id);
        }

        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Job Role has been deleted successfully" : result.Message };
    }
}