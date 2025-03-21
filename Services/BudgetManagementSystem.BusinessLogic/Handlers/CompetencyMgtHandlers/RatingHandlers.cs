namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetRatingsQueryHandler(IRepo<Rating> repo) : IRequestHandler<GetRatingsQuery, List<RatingVm>>
{
    public async ValueTask<List<RatingVm>> Handle(GetRatingsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Rating> query;

        query = repo.GetAllByQuery(x => x.IsActive.Equals(request.IsActive)).AsNoTracking();

        return await query.Select(s => new RatingVm
        {
            RatingId = s.RatingId,
            Name = s.Name,
            Value = s.Value,
            IsActive = s.IsActive
        }).OrderBy(o => o.Value).ToListAsync(cancellationToken);
    }
}

public class SaveRatingHandler(IRepo<Rating> repo) : IRequestHandler<SaveRatingCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(SaveRatingCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.RatingId > 0)
        {
            var rating = await repo.GetById(request.Vm.RatingId);
            if (rating != null)
            {
                rating.Name = request.Vm.Name;
                rating.Value = request.Vm.Value;
                rating.IsActive = request.Vm.IsActive;

                repo.UpdateRecord(rating);
                message = "Rating has been updated successfully";
            }
        }
        else
        {
            var model = new Rating
            {
                Name = request.Vm.Name,
                Value = request.Vm.Value,
                IsActive = request.Vm.IsActive
            };
            await repo.AddRecord(model);
            message = "Rating has been created successfully";
        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

public class DeleteRatingHandler(IRepo<Rating> repo) : IRequestHandler<DeleteRatingCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(DeleteRatingCmd request, CancellationToken cancellationToken)
    {
        var rating = await repo.GetById(request.Id);

        if (request.IsSoftDelete && rating != null)
        {
            rating.SoftDeleted = true;
            rating.IsActive = false;
            repo.UpdateRecord(rating);
        }
        else
        {
            await repo.Delete(request.Id);
        }

        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Rating has been deleted successfully" : result.Message };
    }
}
