namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetAssignJobGradeGroupsQueryHandler : IRequestHandler<GetAssignJobGradeGroupsQuery, List<AssignJobGradeGroupVm>>
{
    private readonly IRepo<AssignJobGradeGroup> _repo;

    public GetAssignJobGradeGroupsQueryHandler(IRepo<AssignJobGradeGroup> repo)
    {
        _repo = repo;
    }

    public async ValueTask<List<AssignJobGradeGroupVm>> Handle(GetAssignJobGradeGroupsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<AssignJobGradeGroup> query;

        query = _repo.GetAllByQuery(null).Include(i => i.JobGrade).Include(i => i.JobGradeGroup);


        return await query.Select(s => new AssignJobGradeGroupVm
        {
            AssignJobGradeGroupId = s.AssignJobGradeGroupId,
            JobGradeGroupId = s.JobGradeGroupId,
            JobGradeGroupName = s.JobGradeGroup.GroupName,
            JobGradeId = s.JobGradeId,
            JobGradeName = s.JobGrade.GradeName,
            IsActive = s.IsActive
        }).OrderBy(o => o.JobGradeName).ToListAsync();
    }
}


public class GetAssignJobGradeGroupHandler : IRequestHandler<GetAssignJobGradeGroupQuery, AssignJobGradeGroupVm>
{
    private readonly IRepo<AssignJobGradeGroup> _repo;

    public GetAssignJobGradeGroupHandler(IRepo<AssignJobGradeGroup> repo)
    {
        _repo = repo;
    }

    public async ValueTask<AssignJobGradeGroupVm> Handle(GetAssignJobGradeGroupQuery request, CancellationToken cancellationToken)
    {
        IQueryable<AssignJobGradeGroup> query;

        query = _repo.GetAllByQuery(x => x.JobGrade.GradeName.Equals(request.GradeName)).Include(i => i.JobGrade).Include(i => i.JobGradeGroup);

        return await query.Select(s => new AssignJobGradeGroupVm
        {
            AssignJobGradeGroupId = s.AssignJobGradeGroupId,
            JobGradeGroupId = s.JobGradeGroupId,
            JobGradeGroupName = s.JobGradeGroup.GroupName,
            JobGradeId = s.JobGradeId,
            JobGradeName = s.JobGrade.GradeName,
            IsActive = s.IsActive
        }).OrderBy(o => o.JobGradeName).FirstOrDefaultAsync();
    }
}


public class GetJobGradeGroupByGradeNameHandler : IRequestHandler<GetJobGradeGroupByGradeNameQuery, List<AssignJobGradeGroupVm>>
{
    private readonly IRepo<AssignJobGradeGroup> _repo;
    private readonly IRepo<JobGradeGroup> _gradeGroupRepo;

    public GetJobGradeGroupByGradeNameHandler(IRepo<AssignJobGradeGroup> repo, IRepo<JobGradeGroup> gradeGroupRepo)
    {
        _repo = repo;
        _gradeGroupRepo = gradeGroupRepo;
    }

    public async ValueTask<List<AssignJobGradeGroupVm>> Handle(GetJobGradeGroupByGradeNameQuery request, CancellationToken cancellationToken)
    {
        IQueryable<AssignJobGradeGroup> query;


        var assignedGrade = await _repo.GetAllByQuery(x => x.JobGrade.GradeName.Equals(request.GradeName))
                                            .Include(i => i.JobGrade).Include(i => i.JobGradeGroup).FirstOrDefaultAsync();
        if (assignedGrade != null)
        {
            if (request.IsSuperior.HasValue)
            {
                int gradeOrder = request.IsSuperior.Equals(true) ? assignedGrade.JobGradeGroup.Order - 1 : assignedGrade.JobGradeGroup.Order + 1;
                var gradeGroup = await _gradeGroupRepo.GetFirstOrDefaultAsync(s => s.Order.Equals(gradeOrder));
                if (gradeOrder > 0 && gradeOrder < 5)
                {
                    query = _repo.GetAllByQuery(x => x.JobGradeGroupId.Equals(gradeGroup.JobGradeGroupId)).Include(i => i.JobGrade).Include(i => i.JobGradeGroup);
                }
                else
                {
                    return [];
                }
            }
            else
            {
                query = _repo.GetAllByQuery(x => x.JobGradeGroupId.Equals(assignedGrade.JobGradeGroupId)).Include(i => i.JobGrade).Include(i => i.JobGradeGroup);
            }
        }
        else
        {
            query = _repo.GetAllByQuery(x => x.JobGrade.GradeName.Equals(request.GradeName)).Include(i => i.JobGradeGroup)
                                    .Include(i => i.JobGrade);
        }


        return await query.Select(s => new AssignJobGradeGroupVm
        {
            AssignJobGradeGroupId = s.AssignJobGradeGroupId,
            JobGradeGroupId = s.JobGradeGroupId,
            JobGradeGroupName = s.JobGradeGroup.GroupName,
            JobGradeId = s.JobGradeId,
            JobGradeName = s.JobGrade.GradeName,
            IsActive = s.IsActive
        }).ToListAsync();
    }
}

public class SaveAssignJobGradeGroupHandler : IRequestHandler<SaveAssignJobGradeGroupCmd, ResponseVm>
{
    private readonly IRepo<AssignJobGradeGroup> _repo;

    public SaveAssignJobGradeGroupHandler(IRepo<AssignJobGradeGroup> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(SaveAssignJobGradeGroupCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.AssignJobGradeGroupId > 0)
        {
            var competencyCategory = await _repo.GetById(request.Vm.AssignJobGradeGroupId);
            if (competencyCategory != null)
            {
                competencyCategory.JobGradeId = request.Vm.JobGradeId;
                competencyCategory.JobGradeGroupId = request.Vm.JobGradeGroupId;
                competencyCategory.IsActive = request.Vm.IsActive;

                _repo.UpdateRecord(competencyCategory);
                message = $"Grade group assigned successfully";
            }
        }
        else
        {
            var model = new AssignJobGradeGroup
            {
                JobGradeGroupId = request.Vm.JobGradeGroupId,
                JobGradeId = request.Vm.JobGradeId,
                IsActive = request.Vm.IsActive,
            };
            await _repo.AddRecord(model);
            message = $"Grade group assigned successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteAssignJobGradeGroupHandler : IRequestHandler<DeleteAssignJobGradeGroupCmd, ResponseVm>
{
    private readonly IRepo<AssignJobGradeGroup> _repo;

    public DeleteAssignJobGradeGroupHandler(IRepo<AssignJobGradeGroup> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteAssignJobGradeGroupCmd request, CancellationToken cancellationToken)
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
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? "Assigned Grade Group has been deleted successfully" : result.Message };
    }
}