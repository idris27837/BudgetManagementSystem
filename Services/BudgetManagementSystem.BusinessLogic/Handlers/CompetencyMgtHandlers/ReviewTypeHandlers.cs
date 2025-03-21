namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;
public class GetReviewTypesQueryHandler(IRepo<ReviewType> repo) : IRequestHandler<GetReviewTypesQuery, List<ReviewTypeVm>>
{
    public async ValueTask<List<ReviewTypeVm>> Handle(GetReviewTypesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ReviewType> query;

        query = repo.GetAllByQuery(null);

        return await query.Select(s => new ReviewTypeVm
        {
            ReviewTypeId = s.ReviewTypeId,
            ReviewTypeName = s.ReviewTypeName,
            IsActive = s.IsActive
        }).ToListAsync(cancellationToken);
    }
}

public class SaveReviewTypeHandler(IRepo<ReviewType> repo) : IRequestHandler<SaveReviewTypeCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(SaveReviewTypeCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.ReviewTypeId > 0)
        {
            var ReviewType = await repo.GetById(request.Vm.ReviewTypeId);
            if (ReviewType != null)
            {
                ReviewType.ReviewTypeName = request.Vm.ReviewTypeName.ToString();
                ReviewType.IsActive = request.Vm.IsActive;

                repo.UpdateRecord(ReviewType);
                message = "Review Type has been updated successfully";
            }
        }
        else
        {
            var model = new ReviewType
            {
                ReviewTypeName = request.Vm.ReviewTypeName.ToString(),
                IsActive = request.Vm.IsActive
            };
            await repo.AddRecord(model);
            message = "Review Type has been created successfully";
        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteReviewTypeHandler(IRepo<ReviewType> repo) : IRequestHandler<DeleteReviewTypeCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(DeleteReviewTypeCmd request, CancellationToken cancellationToken)
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
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Review Type has been deleted successfully" : result.Message };
    }
}