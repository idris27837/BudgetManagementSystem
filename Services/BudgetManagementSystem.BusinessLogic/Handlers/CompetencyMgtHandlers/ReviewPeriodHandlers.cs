namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetReviewPeriodsQueryHandler(IRepo<ReviewPeriod> repo) : IRequestHandler<GetReviewPeriodsQuery, List<ReviewPeriodVm>>
{
    public async ValueTask<List<ReviewPeriodVm>> Handle(GetReviewPeriodsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ReviewPeriod> query;

        query = repo.GetAllByQuery(null).Include(i => i.BankYear);

        return await query.Select(s => new ReviewPeriodVm
        {
            ReviewPeriodId = s.ReviewPeriodId,
            BankYearId = s.BankYearId,
            BankYearName = s.BankYear.YearName,
            Name = s.Name,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            IsActive = s.IsActive,
            IsApproved = s.IsApproved,
            ApprovedBy = s.ApprovedBy,
            DateApproved = s.DateApproved
        }).ToListAsync(cancellationToken);
    }
}
public class GetCurrentReviewPeriodHandler(IRepo<ReviewPeriod> repo) : IRequestHandler<GetCurrentReviewPeriodQuery, ReviewPeriodVm>
{
    public async ValueTask<ReviewPeriodVm> Handle(GetCurrentReviewPeriodQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ReviewPeriod> query;

        query = repo.GetAllByQuery(x => x.IsApproved.Equals(true) && x.IsActive.Equals(true)).Include(i => i.BankYear);

        return await query.Select(s => new ReviewPeriodVm
        {
            ReviewPeriodId = s.ReviewPeriodId,
            BankYearId = s.BankYearId,
            BankYearName = s.BankYear.YearName,
            Name = s.Name,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            IsActive = s.IsActive,
            IsApproved = s.IsApproved,
        }).FirstOrDefaultAsync(cancellationToken);
    }
}

public class SaveReviewPeriodHandler(IRepo<ReviewPeriod> repo) : IRequestHandler<SaveReviewPeriodCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(SaveReviewPeriodCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.IsActive)
        {
            await DeActiveCurrentActive();
        }

        if (request.Vm.ReviewPeriodId > 0)
        {
            var reviewPeriod = await repo.GetById(request.Vm.ReviewPeriodId);
            if (reviewPeriod != null)
            {
                reviewPeriod.Name = request.Vm.Name;
                reviewPeriod.BankYearId = request.Vm.BankYearId;
                reviewPeriod.StartDate = request.Vm.StartDate;
                reviewPeriod.EndDate = request.Vm.EndDate;
                reviewPeriod.IsActive = request.Vm.IsActive;
                reviewPeriod.IsApproved = request.Vm.IsApproved;
                reviewPeriod.ApprovedBy = request.Vm.ApprovedBy;
                reviewPeriod.DateApproved = request.Vm.DateApproved;

                repo.UpdateRecord(reviewPeriod);
                message = "Review Period has been updated successfully";
            }
        }
        else
        {
            var model = new ReviewPeriod
            {
                Name = request.Vm.Name,
                BankYearId = request.Vm.BankYearId,
                StartDate = request.Vm.StartDate,
                EndDate = request.Vm.EndDate,
                IsActive = request.Vm.IsActive,
                IsApproved = request.Vm.IsApproved,
                ApprovedBy = request.Vm.ApprovedBy,
                DateApproved = request.Vm.DateApproved,
            };
            await repo.AddRecord(model);
            message = "Review Period has been created successfully";
        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm
        {
            IsSuccess = result.IsSuccess,
            Message = result.IsSuccess ? message : result.Message
        };
    }

    public async ValueTask DeActiveCurrentActive()
    {
        var allActives = repo.GetAllByQuery(x => x.IsActive.Equals(true));
        foreach (var item in allActives)
        {
            item.IsActive = false;
            repo.UpdateRecord(item);
        }
        await repo.SaveContextAsync();
    }
}

public class DeleteReviewPeriodHandler(IRepo<ReviewPeriod> repo) : IRequestHandler<DeleteReviewPeriodCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(DeleteReviewPeriodCmd request, CancellationToken cancellationToken)
    {
        var reviewPeriod = await repo.GetById(request.Id);

        if (request.IsSoftDelete && reviewPeriod != null)
        {
            reviewPeriod.SoftDeleted = true;
            reviewPeriod.IsActive = false;
            repo.UpdateRecord(reviewPeriod);
        }
        else
        {
            await repo.Delete(request.Id);
        }

        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Review Period has been deleted successfully" : result.Message };
    }
}

public class ApproveReviewPeriodHandler(IRepo<ReviewPeriod> repo) : IRequestHandler<ApproveReviewPeriodCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(ApproveReviewPeriodCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;

        var reviewPeriod = await repo.GetById(request.Vm.ReviewPeriodId);
        if (reviewPeriod != null)
        {
            reviewPeriod.IsActive = request.Vm.IsActive;
            reviewPeriod.IsApproved = request.Vm.IsApproved;
            reviewPeriod.ApprovedBy = request.Vm.ApprovedBy;
            reviewPeriod.DateApproved = request.Vm.DateApproved;

            repo.UpdateRecord(reviewPeriod);
            message = "Review Period has been approved successfully";
        }
        else
        {
            return new ResponseVm
            {
                IsSuccess = false,
                Message = "Record with Id not found"
            };
        }

        var result = await repo.SaveContextAsync();
        return new ResponseVm
        {
            IsSuccess = result.IsSuccess,
            Message = result.IsSuccess ? message : result.Message
        };
    }

    public async ValueTask DeActiveCurrentActive()
    {
        var allActives = repo.GetAllByQuery(x => x.IsActive.Equals(true));
        foreach (var item in allActives)
        {
            item.IsActive = false;
            repo.UpdateRecord(item);
        }
        await repo.SaveContextAsync();
    }
}
