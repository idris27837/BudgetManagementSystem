namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetBehavioralCompetenciesQueryHandler : IRequestHandler<GetBehavioralCompetenciesQuery, List<BehavioralCompetencyVm>>
{
    private readonly IRepo<BehavioralCompetency> _repo;

    public GetBehavioralCompetenciesQueryHandler(IRepo<BehavioralCompetency> repo)
    {
        _repo = repo;
    }

    public async ValueTask<List<BehavioralCompetencyVm>> Handle(GetBehavioralCompetenciesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<BehavioralCompetency> query;

        if (!string.IsNullOrWhiteSpace(request.GradeName))
        {
            query = _repo.GetAllByQuery(s => s.JobGradeGroup.GroupName.Equals(request.GradeName)).Include(i => i.Competency).Include(i => i.JobGradeGroup)
                      .Include(i => i.Rating);
        }
        else
        {
            query = _repo.GetAllByQuery(null).Include(i => i.Competency).Include(i => i.JobGradeGroup)
                      .Include(i => i.Rating);
        }

        return await query.Select(s => new BehavioralCompetencyVm
        {
            BehavioralCompetencyId = s.BehavioralCompetencyId,
            CompetencyId = s.CompetencyId,
            CompetencyName = s.Competency.CompetencyName,
            RatingId = s.RatingId,
            RatingName = s.Rating.Name,
            JobGradeGroupId = s.JobGradeGroupId,
            JobGradeGroupName = s.JobGradeGroup.GroupName,
            IsActive = s.IsActive
        }).ToListAsync();
    }
}


public class SaveBehavioralCompetencyHandler : IRequestHandler<SaveBehavioralCompetencyCmd, ResponseVm>
{
    private readonly IRepo<BehavioralCompetency> _repo;

    public SaveBehavioralCompetencyHandler(IRepo<BehavioralCompetency> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(SaveBehavioralCompetencyCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.BehavioralCompetencyId > 0)
        {
            var jobRoleCompetency = await _repo.GetById(request.Vm.BehavioralCompetencyId);
            if (jobRoleCompetency != null)
            {
                jobRoleCompetency.BehavioralCompetencyId = request.Vm.BehavioralCompetencyId;
                jobRoleCompetency.CompetencyId = request.Vm.CompetencyId;
                jobRoleCompetency.JobGradeGroupId = request.Vm.JobGradeGroupId;
                jobRoleCompetency.RatingId = request.Vm.RatingId;
                jobRoleCompetency.IsActive = request.Vm.IsActive;

                _repo.UpdateRecord(jobRoleCompetency);
                message = $"{request.Vm.CompetencyName} has been added to the {request.Vm.JobGradeGroupName} successfully";
            }
        }
        else
        {
            var model = new BehavioralCompetency
            {
                BehavioralCompetencyId = request.Vm.BehavioralCompetencyId,
                CompetencyId = request.Vm.CompetencyId,
                JobGradeGroupId = request.Vm.JobGradeGroupId,
                RatingId = request.Vm.RatingId,
                IsActive = request.Vm.IsActive
            };
            await _repo.AddRecord(model);
            message = "Added successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : $"{request.Vm.CompetencyName} already exist for {request.Vm.JobGradeGroupName}" };
    }
}

public class DeleteBehavioralCompetencyHandler : IRequestHandler<DeleteBehavioralCompetencyCmd, ResponseVm>
{
    private readonly IRepo<BehavioralCompetency> _repo;

    public DeleteBehavioralCompetencyHandler(IRepo<BehavioralCompetency> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteBehavioralCompetencyCmd request, CancellationToken cancellationToken)
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

