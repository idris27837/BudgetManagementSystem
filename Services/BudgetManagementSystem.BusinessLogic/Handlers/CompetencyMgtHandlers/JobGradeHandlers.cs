namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetJobGradesQueryHandler : IRequestHandler<GetJobGradesQuery, List<JobGradeVm>>
{
    private readonly IRepo<JobGrade> _repo;

    public GetJobGradesQueryHandler(IRepo<JobGrade> repo)
    {
        _repo = repo;
    }

    public async ValueTask<List<JobGradeVm>> Handle(GetJobGradesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<JobGrade> query;

        query = _repo.GetAllByQuery(null);

        return await query.Select(s => new JobGradeVm
        {
            JobGradeId = s.JobGradeId,
            GradeCode = s.GradeCode,
            GradeName = s.GradeName,
            IsActive = s.IsActive
        }).OrderBy(o => o.GradeName).ToListAsync();
    }
}

public class SaveJobGradeHandler : IRequestHandler<SaveJobGradeCmd, ResponseVm>
{
    private readonly IRepo<JobGrade> _repo;

    public SaveJobGradeHandler(IRepo<JobGrade> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(SaveJobGradeCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.JobGradeId > 0)
        {
            var competencyCategory = await _repo.GetById(request.Vm.JobGradeId);
            if (competencyCategory != null)
            {
                competencyCategory.GradeName = request.Vm.GradeName.ToUpperString();
                competencyCategory.GradeCode = request.Vm.GradeCode;
                competencyCategory.IsActive = request.Vm.IsActive;

                _repo.UpdateRecord(competencyCategory);
                message = $"{competencyCategory.GradeName} Grade has been updated successfully";
            }
        }
        else
        {
            var model = new JobGrade
            {
                GradeName = request.Vm.GradeName.ToUpperString(),
                GradeCode = request.Vm.GradeCode,
                IsActive = request.Vm.IsActive,
            };
            await _repo.AddRecord(model);
            message = $"{model.GradeName} Grade has been created successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteJobGradeHandler : IRequestHandler<DeleteJobGradeCmd, ResponseVm>
{
    private readonly IRepo<JobGrade> _repo;

    public DeleteJobGradeHandler(IRepo<JobGrade> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteJobGradeCmd request, CancellationToken cancellationToken)
    {
        var competencyCategory = await _repo.GetById(request.Id);

        if (request.IsSoftDelete && competencyCategory != null)
        {
            competencyCategory.SoftDeleted = true;
            competencyCategory.IsActive = false;
            _repo.UpdateRecord(competencyCategory);
        }
        else
        {
            await _repo.Delete(request.Id);
        }

        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"{competencyCategory.GradeName} Grade has been deleted successfully" : result.Message };
    }
}

