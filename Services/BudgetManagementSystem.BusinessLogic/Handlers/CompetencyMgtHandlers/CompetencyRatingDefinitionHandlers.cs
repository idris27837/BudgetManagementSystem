
namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetCompetencyRatingDefinitionsHandler(IRepo<CompetencyRatingDefinition> repo) : IRequestHandler<GetCompetencyRatingDefinitionsQuery, List<CompetencyRatingDefinitionVm>>
{
    public async ValueTask<List<CompetencyRatingDefinitionVm>> Handle(GetCompetencyRatingDefinitionsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<CompetencyRatingDefinition> query;

        if (request.CompetencyId.HasValue)
        {
            query = repo.GetAllByQuery(x => x.CompetencyId.Equals((int)request.CompetencyId)).Include(i => i.Rating).Include(i => i.Competency);
        }
        else
        {
            query = repo.GetAllByQuery(null).Include(i => i.Rating).Include(i => i.Competency);
        }


        return await query.Select(s => new CompetencyRatingDefinitionVm
        {
            CompetencyRatingDefinitionId = s.CompetencyRatingDefinitionId,
            CompetencyId = s.CompetencyId,
            CompetencyName = s.Competency.CompetencyName,
            RatingId = s.RatingId,
            RatingName = s.Rating.Name,
            RatingValue = s.Rating.Value,
            Definition = s.Definition,
            IsActive = s.IsActive
        }).OrderBy(o => o.RatingValue).ToListAsync(cancellationToken);
    }
}

public class SaveCompetencyRatingDefinitionHandler(IRepo<CompetencyRatingDefinition> repo) : IRequestHandler<SaveCompetencyRatingDefinitionCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(SaveCompetencyRatingDefinitionCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.CompetencyRatingDefinitionId > 0)
        {
            var competencyRatingDefinition = await repo.GetById(request.Vm.CompetencyRatingDefinitionId);
            if (competencyRatingDefinition != null)
            {
                competencyRatingDefinition.CompetencyId = request.Vm.CompetencyId;
                competencyRatingDefinition.RatingId = request.Vm.RatingId;
                competencyRatingDefinition.Definition = request.Vm.Definition;
                competencyRatingDefinition.IsActive = request.Vm.IsActive;

                repo.UpdateRecord(competencyRatingDefinition);
                message = "Competency Rating definition has been updated successfully";
            }
        }
        else
        {
            var model = new CompetencyRatingDefinition
            {
                CompetencyId = request.Vm.CompetencyId,
                RatingId = request.Vm.RatingId,
                Definition = request.Vm.Definition,
                IsActive = request.Vm.IsActive,
            };
            await repo.AddRecord(model);
            message = "Competency Rating definition has been Created Successfully";
        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteCompetencyRatingDefinitionHandler(IRepo<CompetencyRatingDefinition> repo) : IRequestHandler<DeleteCompetencyRatingDefinitionCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(DeleteCompetencyRatingDefinitionCmd request, CancellationToken cancellationToken)
    {
        var competencyRatingDefinition = await repo.GetById(request.Id);

        if (request.IsSoftDelete && competencyRatingDefinition != null)
        {
            competencyRatingDefinition.SoftDeleted = true;
            competencyRatingDefinition.IsActive = false;
            repo.UpdateRecord(competencyRatingDefinition);
        }
        else
        {
            await repo.Delete(request.Id);
        }

        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Competency Rating definition has been updated successfully" : result.Message };
    }
}

