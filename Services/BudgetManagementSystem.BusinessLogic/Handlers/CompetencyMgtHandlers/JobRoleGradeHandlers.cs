namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetJobRoleGradesQueryHandler : IRequestHandler<GetJobRoleGradesQuery, List<JobRoleGradeVm>>
{
    private readonly IRepo<JobRoleGrade> _repo;

    public GetJobRoleGradesQueryHandler(IRepo<JobRoleGrade> repo)
    {
        _repo = repo;
    }

    public async ValueTask<List<JobRoleGradeVm>> Handle(GetJobRoleGradesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<JobRoleGrade> query;

        query = _repo.GetAllByQuery(null);

        return await query.Select(s => new JobRoleGradeVm
        {
            JobRoleGradeId = s.JobRoleGradeId,
            JobRoleId = s.JobRoleId,
            GradeId = s.GradeId,
            GradeName = s.GradeName,
            IsActive = s.IsActive
        }).ToListAsync();
    }
}

public class SaveJobRoleGradeHandler : IRequestHandler<SaveJobRoleGradeCmd, ResponseVm>
{
    private readonly IRepo<JobRoleGrade> _repo;

    public SaveJobRoleGradeHandler(IRepo<JobRoleGrade> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(SaveJobRoleGradeCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.JobRoleGradeId > 0)
        {
            var jobRoleGrade = await _repo.GetById(request.Vm.JobRoleGradeId);
            if (jobRoleGrade != null)
            {
                jobRoleGrade.JobRoleId = request.Vm.JobRoleId;
                jobRoleGrade.GradeId = request.Vm.GradeId;
                jobRoleGrade.GradeName = request.Vm.GradeName;
                jobRoleGrade.IsActive = request.Vm.IsActive;

                _repo.UpdateRecord(jobRoleGrade);
                message = "Job Role Grade has been updated successfully";
            }
        }
        else
        {
            var model = new JobRoleGrade
            {
                JobRoleId = request.Vm.JobRoleId,
                GradeId = request.Vm.GradeId,
                GradeName = request.Vm.GradeName,
                IsActive = request.Vm.IsActive
            };
            await _repo.AddRecord(model);
            message = "Job Role Grade has been created successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteJobRoleGradeHandler : IRequestHandler<DeleteJobRoleGradeCmd, ResponseVm>
{
    private readonly IRepo<JobRoleGrade> _repo;

    public DeleteJobRoleGradeHandler(IRepo<JobRoleGrade> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteJobRoleGradeCmd request, CancellationToken cancellationToken)
    {
        var jobRoleGrade = await _repo.GetById(request.Id);

        if (request.IsSoftDelete && jobRoleGrade != null)
        {
            jobRoleGrade.SoftDeleted = true;
            jobRoleGrade.IsActive = false;
            _repo.UpdateRecord(jobRoleGrade);
        }
        else
        {
            await _repo.Delete(request.Id);
        }

        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Job Role Grade has been deleted successfully" : result.Message };
    }
}