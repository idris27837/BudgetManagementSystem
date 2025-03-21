namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetCompetencyCategorysQueryHandler : IRequestHandler<GetCompetencyCategoriesQuery, List<CompetencyCategoryVm>>
{
    private readonly IRepo<CompetencyCategory> _repo;

    public GetCompetencyCategorysQueryHandler(IRepo<CompetencyCategory> repo)
    {
        _repo = repo;
    }

    public async ValueTask<List<CompetencyCategoryVm>> Handle(GetCompetencyCategoriesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<CompetencyCategory> query;

        query = _repo.GetAllByQuery(null);

        return await query.Select(s => new CompetencyCategoryVm
        {
            CompetencyCategoryId = s.CompetencyCategoryId,
            CategoryName = s.CategoryName,
            IsTechnical = s.IsTechnical,
            IsActive = s.IsActive,
        }).ToListAsync();
    }
}


public class SaveCompetencyCategoryHandler : IRequestHandler<SaveCompetencyCategoryCmd, ResponseVm>
{
    private readonly IRepo<CompetencyCategory> _repo;

    public SaveCompetencyCategoryHandler(IRepo<CompetencyCategory> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(SaveCompetencyCategoryCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.CompetencyCategoryId > 0)
        {
            var competencyCategory = await _repo.GetById(request.Vm.CompetencyCategoryId);
            if (competencyCategory != null)
            {
                competencyCategory.CategoryName = request.Vm.CategoryName.ToUpperString();
                competencyCategory.IsTechnical = request.Vm.IsTechnical;
                competencyCategory.IsActive = request.Vm.IsActive;

                _repo.UpdateRecord(competencyCategory);
                message = $"{competencyCategory.CategoryName} Competency Category has been updated successfully";
            }
        }
        else
        {
            var model = new CompetencyCategory
            {
                CategoryName = request.Vm.CategoryName.ToUpperString(),
                IsTechnical = request.Vm.IsTechnical,
                IsActive = request.Vm.IsActive,
            };
            await _repo.AddRecord(model);
            message = $"{model.CategoryName} Competency Category has been created successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteCompetencyCategoryHandler : IRequestHandler<DeleteCompetencyCategoryCmd, ResponseVm>
{
    private readonly IRepo<CompetencyCategory> _repo;

    public DeleteCompetencyCategoryHandler(IRepo<CompetencyCategory> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteCompetencyCategoryCmd request, CancellationToken cancellationToken)
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
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"{competencyCategory.CategoryName} Competency Category has been deleted successfully" : result.Message };
    }
}

