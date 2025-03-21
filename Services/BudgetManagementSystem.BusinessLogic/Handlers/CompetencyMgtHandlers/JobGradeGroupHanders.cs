namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;


public class GetJobGradeGroupsQueryHandler : IRequestHandler<GetJobGradeGroupsQuery, List<JobGradeGroupVm>>
{
    private readonly IRepo<JobGradeGroup> _repo;

    public GetJobGradeGroupsQueryHandler(IRepo<JobGradeGroup> repo)
    {
        _repo = repo;
    }

    public async ValueTask<List<JobGradeGroupVm>> Handle(GetJobGradeGroupsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<JobGradeGroup> query;

        query = _repo.GetAllByQuery(null);

        return await query.Select(s => new JobGradeGroupVm
        {
            JobGradeGroupId = s.JobGradeGroupId,
            GroupName = s.GroupName,
            Order = s.Order,
            IsActive = s.IsActive
        }).OrderBy(o => o.Order).ToListAsync();
    }
}

public class SaveJobGradeGroupHandler : IRequestHandler<SaveJobGradeGroupCmd, ResponseVm>
{
    private readonly IRepo<JobGradeGroup> _repo;

    public SaveJobGradeGroupHandler(IRepo<JobGradeGroup> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(SaveJobGradeGroupCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.JobGradeGroupId > 0)
        {
            var competencyCategory = await _repo.GetById(request.Vm.JobGradeGroupId);
            if (competencyCategory != null)
            {
                competencyCategory.GroupName = request.Vm.GroupName.ToUpperString();
                competencyCategory.Order = request.Vm.Order;
                competencyCategory.IsActive = request.Vm.IsActive;

                _repo.UpdateRecord(competencyCategory);
                message = $"{competencyCategory.GroupName} Grade Group has been updated successfully";
            }
        }
        else
        {
            var model = new JobGradeGroup
            {
                GroupName = request.Vm.GroupName.ToUpperString(),
                Order = request.Vm.Order,
                IsActive = request.Vm.IsActive,
            };
            await _repo.AddRecord(model);
            message = $"{model.GroupName} Grade Group has been created successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteJobGradeGroupHandler : IRequestHandler<DeleteJobGradeGroupCmd, ResponseVm>
{
    private readonly IRepo<JobGradeGroup> _repo;

    public DeleteJobGradeGroupHandler(IRepo<JobGradeGroup> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteJobGradeGroupCmd request, CancellationToken cancellationToken)
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
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"{competencyCategory.GroupName} Grade Group has been deleted successfully" : result.Message };
    }
}