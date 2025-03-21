namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetCompetencyCategoryGradingsQueryHandler : IRequestHandler<GetCompetencyCategoryGradingsQuery, List<CompetencyCategoryGradingVm>>
{
    private readonly IRepo<CompetencyCategoryGrading> _repo;

    public GetCompetencyCategoryGradingsQueryHandler(IRepo<CompetencyCategoryGrading> repo)
    {
        _repo = repo;
    }

    public async ValueTask<List<CompetencyCategoryGradingVm>> Handle(GetCompetencyCategoryGradingsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<CompetencyCategoryGrading> query;

        query = _repo.GetAllByQuery(null).Include(i => i.CompetencyCategory);

        return await query.Select(s => new CompetencyCategoryGradingVm
        {
            CompetencyCategoryGradingId = s.CompetencyCategoryGradingId,
            CompetencyCategoryId = s.CompetencyCategoryId,
            CompetencyCategoryName = s.CompetencyCategory.CategoryName,
            ReviewTypeId = s.ReviewTypeId,
            ReviewTypeName = s.ReviewType.ReviewTypeName,
            WeightPercentage = s.WeightPercentage,
            IsActive = s.IsActive
        }).ToListAsync();
    }
}


public class SaveCompetencyCategoryGradingHandler : IRequestHandler<SaveCompetencyCategoryGradingCmd, ResponseVm>
{
    private readonly IRepo<CompetencyCategoryGrading> _repo;

    public SaveCompetencyCategoryGradingHandler(IRepo<CompetencyCategoryGrading> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(SaveCompetencyCategoryGradingCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.CompetencyCategoryGradingId > 0)
        {
            var competencyCategoryGrading = await _repo.GetById(request.Vm.CompetencyCategoryGradingId);
            if (competencyCategoryGrading != null)
            {
                competencyCategoryGrading.CompetencyCategoryId = request.Vm.CompetencyCategoryId;
                competencyCategoryGrading.ReviewTypeId = request.Vm.ReviewTypeId;
                competencyCategoryGrading.WeightPercentage = request.Vm.WeightPercentage;
                competencyCategoryGrading.IsActive = request.Vm.IsActive;

                _repo.UpdateRecord(competencyCategoryGrading);
                message = "Competency Category Grading has been updated successfully";
            }
        }
        else
        {
            var model = new CompetencyCategoryGrading
            {
                CompetencyCategoryId = request.Vm.CompetencyCategoryId,
                ReviewTypeId = request.Vm.ReviewTypeId,
                WeightPercentage = request.Vm.WeightPercentage,
                IsActive = request.Vm.IsActive,
            };
            await _repo.AddRecord(model);
            message = "Competency Category Grading has been updated successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteCompetencyCategoryGradingHandler : IRequestHandler<DeleteCompetencyCategoryGradingCmd, ResponseVm>
{
    private readonly IRepo<CompetencyCategoryGrading> _repo;

    public DeleteCompetencyCategoryGradingHandler(IRepo<CompetencyCategoryGrading> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteCompetencyCategoryGradingCmd request, CancellationToken cancellationToken)
    {
        var competencyCategoryGrading = await _repo.GetById(request.Id);

        if (request.IsSoftDelete && competencyCategoryGrading != null)
        {
            competencyCategoryGrading.SoftDeleted = true;
            competencyCategoryGrading.IsActive = false;
            _repo.UpdateRecord(competencyCategoryGrading);
        }
        else
        {
            await _repo.Delete(request.Id);
        }

        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Competency Category Grading has been deleeted successfully" : result.Message };
    }
}
