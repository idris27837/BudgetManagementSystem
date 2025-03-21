namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetCompetencyReviewsHandler : IRequestHandler<GetCompetencyReviewsQuery, List<CompetencyReviewVm>>
{
    private readonly IRepo<CompetencyReview> _repo;

    public GetCompetencyReviewsHandler(IRepo<CompetencyReview> repo)
    {
        _repo = repo;
    }

    public async ValueTask<List<CompetencyReviewVm>> Handle(GetCompetencyReviewsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<CompetencyReview> query;

        query = _repo.GetAllByQuery(null).Include(i => i.Competency).Include(i => i.ReviewType).Include(i => i.ReviewPeriod);

        return await query.Select(s => new CompetencyReviewVm
        {
            EmployeeNumber = s.EmployeeNumber,
            CompetencyReviewId = s.CompetencyReviewId,
            CompetencyId = s.CompetencyId,
            CompetencyName = s.Competency.CompetencyName,
            ReviewDate = s.ReviewDate,
            ReviewerId = s.ReviewerId,
            ReviewTypeId = s.ReviewTypeId,
            ReviewTypeName = s.ReviewType.ReviewTypeName.ToString(),
            ReviewPeriodId = s.ReviewPeriodId,
            ReviewPeriodName = s.ReviewPeriod.Name,
            ReviewerName = s.ReviewerName,
            ActualRatingId = s.ActualRatingId,
            ActualRatingName = s.ActualRatingName,
            ActualRatingValue = s.ActualRatingValue,
            ExpectedRatingId = s.ExpectedRatingId,
            ExpectedRatingName = s.ExpectedRating.Name,
            ExpectedRatingValue = s.ExpectedRating.Value,
            IsTechnical = s.IsTechnical,
            EmployeeName = s.EmployeeName,
            EmployeeDepartment = s.EmployeeDepartment,
            EmployeeInitial = s.EmployeeInitial,
            EmployeeGrade = s.EmployeeGrade,
            IsActive = s.IsActive
        }).ToListAsync();
    }
}

public class GetCompetencyReviewByReviewerHandler : IRequestHandler<GetCompetencyReviewByReviewerQuery, List<CompetencyReviewVm>>
{
    private readonly IRepo<CompetencyReview> _repo;

    public GetCompetencyReviewByReviewerHandler(IRepo<CompetencyReview> repo)
    {
        _repo = repo;
    }

    public async ValueTask<List<CompetencyReviewVm>> Handle(GetCompetencyReviewByReviewerQuery request, CancellationToken cancellationToken)
    {
        IQueryable<CompetencyReview> query;
        if (request.ReviewPeriodId.HasValue)
        {
            query = _repo.GetAllByQuery(x => x.ReviewerId.Equals(request.ReviewerId) && x.ReviewPeriodId.Equals((int)request.ReviewPeriodId))
                                .Include(i => i.Competency.CompetencyCategory).Include(i => i.ReviewType).Include(i => i.ReviewPeriod);
        }
        else
        {
            query = _repo.GetAllByQuery(x => x.ReviewerId.Equals(request.ReviewerId))
                                .Include(i => i.Competency.CompetencyCategory).Include(i => i.ReviewType).Include(i => i.ReviewPeriod);
        }

        return await query.Select(s => new CompetencyReviewVm
        {
            CompetencyCategoryName = s.Competency.CompetencyCategory.CategoryName,
            EmployeeNumber = s.EmployeeNumber,
            CompetencyReviewId = s.CompetencyReviewId,
            CompetencyId = s.CompetencyId,
            CompetencyName = s.Competency.CompetencyName,
            ReviewDate = s.ReviewDate,
            ReviewerId = s.ReviewerId,
            ReviewTypeId = s.ReviewTypeId,
            ReviewTypeName = s.ReviewType.ReviewTypeName.ToString(),
            ReviewPeriodId = s.ReviewPeriodId,
            ReviewPeriodName = s.ReviewPeriod.Name,
            ReviewerName = s.ReviewerName,
            ActualRatingId = s.ActualRatingId,
            ActualRatingName = s.ActualRatingName,
            ActualRatingValue = s.ActualRatingValue,
            ExpectedRatingId = s.ExpectedRatingId,
            ExpectedRatingName = s.ExpectedRating.Name,
            ExpectedRatingValue = s.ExpectedRating.Value,
            IsTechnical = s.IsTechnical,
            EmployeeName = s.EmployeeName,
            EmployeeDepartment = s.EmployeeDepartment,
            EmployeeInitial = s.EmployeeInitial,
            EmployeeGrade = s.EmployeeGrade,
            IsActive = s.IsActive
        }).ToListAsync();
    }
}

public class GetCompetencyReviewForEmployeeHandler : IRequestHandler<GetCompetencyReviewForEmployeeQuery, List<CompetencyReviewVm>>
{
    private readonly IRepo<CompetencyReview> _repo;

    public GetCompetencyReviewForEmployeeHandler(IRepo<CompetencyReview> repo)
    {
        _repo = repo;
    }

    public async ValueTask<List<CompetencyReviewVm>> Handle(GetCompetencyReviewForEmployeeQuery request, CancellationToken cancellationToken)
    {
        IQueryable<CompetencyReview> query;
        if (request.ReviewPeriodId.HasValue)
        {
            query = _repo.GetAllByQuery(x => x.EmployeeNumber.Equals(request.EmployeeNumber) && x.ReviewPeriodId.Equals((int)request.ReviewPeriodId))
                                .Include(i => i.Competency).Include(i => i.ReviewType).Include(i => i.ReviewPeriod);
        }
        else
        {
            query = _repo.GetAllByQuery(x => x.EmployeeNumber.Equals(request.EmployeeNumber))
                                .Include(i => i.Competency).Include(i => i.ReviewType).Include(i => i.ReviewPeriod);
        }

        return await query.Select(s => new CompetencyReviewVm
        {
            EmployeeNumber = s.EmployeeNumber,
            CompetencyReviewId = s.CompetencyReviewId,
            CompetencyId = s.CompetencyId,
            CompetencyName = s.Competency.CompetencyName,
            ReviewDate = s.ReviewDate,
            ReviewerId = s.ReviewerId,
            ReviewTypeId = s.ReviewTypeId,
            ReviewTypeName = s.ReviewType.ReviewTypeName.ToString(),
            ReviewPeriodId = s.ReviewPeriodId,
            ReviewPeriodName = s.ReviewPeriod.Name,
            ReviewerName = s.ReviewerName,
            ActualRatingId = s.ActualRatingId,
            ActualRatingName = s.ActualRatingName,
            ActualRatingValue = s.ActualRatingValue,
            ExpectedRatingId = s.ExpectedRatingId,
            ExpectedRatingName = s.ExpectedRating.Name,
            ExpectedRatingValue = s.ExpectedRating.Value,
            IsTechnical = s.IsTechnical,
            EmployeeName = s.EmployeeName,
            EmployeeDepartment = s.EmployeeDepartment,
            EmployeeInitial = s.EmployeeInitial,
            EmployeeGrade = s.EmployeeGrade,
            IsActive = s.IsActive
        }).ToListAsync();
    }
}


public class GetCompetencyReviewDetailHandler : IRequestHandler<GetCompetencyReviewDetailQuery, CompetencyReviewDetailVm>
{
    private readonly IRepo<CompetencyReview> _repo;

    public GetCompetencyReviewDetailHandler(IRepo<CompetencyReview> repo)
    {
        _repo = repo;
    }

    public async ValueTask<CompetencyReviewDetailVm> Handle(GetCompetencyReviewDetailQuery request, CancellationToken cancellationToken)
    {
        IQueryable<CompetencyReview> query;

        query = _repo.GetAllByQuery(x => x.ReviewerId.Equals(request.Vm.ReviewerId) && x.IsTechnical.Equals(request.Vm.IsTechnical)
                                    && x.EmployeeNumber.Equals(request.Vm.EmployeeId) && x.ReviewPeriodId.Equals(request.Vm.ReviewPeriodId)
                                    && x.ReviewTypeId.Equals(request.Vm.ReviewaTypeId))
                                        .Include(i => i.Competency.CompetencyCategory)
                                        .Include(i => i.ReviewType).Include(i => i.ReviewPeriod)
                                        .Include(i => i.Competency.CompetencyRatingDefinitions).ThenInclude(i => i.Rating);

        var models = await query.Select(s => new CompetencyReviewVm
        {
            EmployeeNumber = s.EmployeeNumber,
            CompetencyReviewId = s.CompetencyReviewId,
            CompetencyId = s.CompetencyId,
            CompetencyName = s.Competency.CompetencyName,
            CompetencyDefinition = s.Competency.Description,
            CompetencyCategoryName = s.Competency.CompetencyCategory.CategoryName,
            ReviewDate = s.ReviewDate,
            ReviewerId = s.ReviewerId,
            ReviewTypeId = s.ReviewTypeId,
            ReviewTypeName = s.ReviewType.ReviewTypeName.ToString(),
            ReviewPeriodId = s.ReviewPeriodId,
            ReviewPeriodName = s.ReviewPeriod.Name,
            ReviewerName = s.ReviewerName,
            ActualRatingId = s.ActualRatingId,
            ActualRatingName = s.ActualRatingName,
            ActualRatingValue = s.ActualRatingValue,
            ExpectedRatingId = s.ExpectedRatingId,
            ExpectedRatingName = s.ExpectedRating.Name,
            ExpectedRatingValue = s.ExpectedRating.Value,
            IsTechnical = s.IsTechnical,
            EmployeeName = s.EmployeeName,
            EmployeeDepartment = s.EmployeeDepartment,
            EmployeeInitial = s.EmployeeInitial,
            EmployeeGrade = s.EmployeeGrade,
            IsActive = s.IsActive,
            CompetencyRatingDefinitions = s.Competency.CompetencyRatingDefinitions.Select(s => new CompetencyRatingDefinitionVm
            {
                RatingName = s.Rating.Name,
                RatingValue = s.Rating.Value,
                Definition = s.Definition
            }).ToList()
        }).ToListAsync();

        return new CompetencyReviewDetailVm { CompetencyReviews = models };
    }
}

public class SaveCompetencyReviewHandler : IRequestHandler<SaveCompetencyReviewCmd, ResponseVm>
{
    private readonly IRepo<CompetencyReview> _repo;

    public SaveCompetencyReviewHandler(IRepo<CompetencyReview> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(SaveCompetencyReviewCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.CompetencyReviewId > 0)
        {
            var competencyReview = await _repo.GetById(request.Vm.CompetencyReviewId);
            if (competencyReview != null)
            {
                competencyReview.CompetencyId = request.Vm.CompetencyId;
                competencyReview.ReviewDate = request.Vm.ReviewDate;
                competencyReview.ExpectedRatingId = request.Vm.ExpectedRatingId;
                competencyReview.ActualRatingId = request.Vm.ActualRatingId;
                competencyReview.ActualRatingName = request.Vm.ActualRatingName;
                competencyReview.ActualRatingValue = request.Vm.ActualRatingValue;
                competencyReview.IsActive = request.Vm.IsActive;

                _repo.UpdateRecord(competencyReview);
                message = "Competency Review has been updated successfully";
            }
        }
        else
        {
            var model = new CompetencyReview
            {
                EmployeeNumber = request.Vm.EmployeeNumber,
                CompetencyId = request.Vm.CompetencyId,
                //RatingId = request.Vm.RatingId,
                ReviewDate = request.Vm.ReviewDate,
                ReviewPeriodId = request.Vm.ReviewPeriodId,
                ReviewTypeId = request.Vm.ReviewTypeId,
                ReviewerId = request.Vm.ReviewerId,
                ReviewerName = request.Vm.ReviewerName,
                IsActive = request.Vm.IsActive,
            };
            await _repo.AddRecord(model);
            message = "Competency Review has been Created Successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm
        {
            IsSuccess = result.IsSuccess,
            Message = result.IsSuccess ? message : result.Message
        };
    }
}

public class DeleteCompetencyReviewHandler : IRequestHandler<DeleteCompetencyReviewCmd, ResponseVm>
{
    private readonly IRepo<CompetencyReview> _repo;

    public DeleteCompetencyReviewHandler(IRepo<CompetencyReview> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteCompetencyReviewCmd request, CancellationToken cancellationToken)
    {
        var competencyReview = await _repo.GetById(request.Id);

        if (request.IsSoftDelete && competencyReview != null)
        {
            competencyReview.SoftDeleted = true;
            competencyReview.IsActive = false;
            _repo.UpdateRecord(competencyReview);
        }
        else
        {
            await _repo.Delete(request.Id);
        }

        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"Competency Review has been updated successfully" : result.Message };
    }
}
