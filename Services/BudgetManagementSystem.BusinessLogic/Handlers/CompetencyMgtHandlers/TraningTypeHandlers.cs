namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetTrainingTypesQueryHandler(IRepo<TrainingType> repo) : IRequestHandler<GetTrainingTypesQuery, List<TrainingTypeVm>>
{
    public async ValueTask<List<TrainingTypeVm>> Handle(GetTrainingTypesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<TrainingType> query;
        if (request.IsActive.HasValue)
        {
            query = repo.GetAllByQuery(x => x.IsActive == request.IsActive.Value);
        }
        else
        {
            query = repo.GetAllByQuery(null);
        }


        return await query.Select(s => new TrainingTypeVm
        {
            TrainingTypeId = s.TrainingTypeId,
            TrainingTypeName = s.TrainingTypeName,
            IsActive = s.IsActive
        }).ToListAsync(cancellationToken);
    }
}

public class SaveTrainingTypeHandler(IRepo<TrainingType> repo) : IRequestHandler<SaveTrainingTypeCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(SaveTrainingTypeCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.TrainingTypeId > 0)
        {
            var TrainingType = await repo.GetById(request.Vm.TrainingTypeId);
            if (TrainingType != null)
            {
                TrainingType.TrainingTypeName = request.Vm.TrainingTypeName.ToString();
                TrainingType.IsActive = request.Vm.IsActive;

                repo.UpdateRecord(TrainingType);
                message = "Development Intervention Type has been updated successfully";
            }
        }
        else
        {
            var model = new TrainingType
            {
                TrainingTypeName = request.Vm.TrainingTypeName.ToString(),
                IsActive = request.Vm.IsActive
            };
            await repo.AddRecord(model);
            message = "Development Intervention Type has been created successfully";
        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteTrainingTypeHandler(IRepo<TrainingType> repo) : IRequestHandler<DeleteTrainingTypeCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(DeleteTrainingTypeCmd request, CancellationToken cancellationToken)
    {
        var reviewType = await repo.GetById(request.Id);

        if (request.IsSoftDelete && reviewType != null)
        {
            reviewType.SoftDeleted = true;
            reviewType.IsActive = false;
            repo.UpdateRecord(reviewType);
        }
        else
        {
            await repo.Delete(request.Id);
        }

        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Development Intervention Type has been deleted successfully" : result.Message };
    }
}
