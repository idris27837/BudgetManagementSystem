using System.Linq.Expressions;

namespace BudgetManagementSystem.BusinessLogic.Handlers.CompetencyMgtHandlers;

public class GetCompetenciesHandler : IRequestHandler<GetCompetenciesQuery, CompetencyListVm>
{
    private readonly IRepo<Competency> _repo;

    public GetCompetenciesHandler(IRepo<Competency> repo)
    {
        _repo = repo;
    }

    public async ValueTask<CompetencyListVm> Handle(GetCompetenciesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Competency> query;
        List<Expression<Func<Competency, bool>>> filters = new();

        if (request.Vm.CategoryId.HasValue && request.Vm.CategoryId > 0)
        {
            filters.Add(y => y.CompetencyCategoryId.Equals((int)request.Vm.CategoryId));
        }

        if (request.Vm.IsTechnical.HasValue)
        {
            filters.Add(y => y.CompetencyCategory.IsTechnical.Equals((bool)request.Vm.IsTechnical));
        }

        if (request.Vm.IsApproved.HasValue)
        {
            filters.Add(y => y.IsApproved.Equals((bool)request.Vm.IsApproved));
        }

        if (request.Vm.IsRejected.HasValue)
        {
            filters.Add(y => y.IsRejected.Equals((bool)request.Vm.IsRejected));
        }


        if (!string.IsNullOrEmpty(request.Vm.SearchString))
        {
            request.Vm.SearchString = request.Vm.SearchString.ToUpperString();
            filters.Add(y => y.CompetencyName.Contains(request.Vm.SearchString));
        }

        query = _repo.GetAllByQueriesPagination(request.Vm.Skip, request.Vm.PageSize, filters).Include(i => i.CompetencyCategory);

        var totalRecords = await _repo.CountFiltersAsync(filters);

        var model = await query.Select(s => new CompetencyVm
        {
            CompetencyId = s.CompetencyId,
            CompetencyName = s.CompetencyName,
            CompetencyCategoryId = s.CompetencyCategoryId,
            CompetencyCategoryName = s.CompetencyCategory.CategoryName,
            Description = s.Description,
            IsApproved = s.IsApproved,
            IsActive = s.IsActive,
            IsRejected = s.IsRejected,
            RejectedBy = s.RejectedBy,
            RejectionReason = s.RejectionReason,
        }).OrderBy(o => o.CompetencyName).ToListAsync();

        return new CompetencyListVm
        {
            TotalRecord = totalRecords,
            Competencies = model
        };
    }
}


public class SaveCompetencyHandler : IRequestHandler<SaveCompetencyCmd, ResponseVm>
{
    private readonly IRepo<Competency> _repo;

    public SaveCompetencyHandler(IRepo<Competency> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(SaveCompetencyCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        Competency competency;
        if (request.Vm.CompetencyId > 0)
        {
            competency = await _repo.GetById(request.Vm.CompetencyId);
            if (competency != null)
            {
                competency.CompetencyCategoryId = request.Vm.CompetencyCategoryId;
                competency.CompetencyName = request.Vm.CompetencyName.ToUpperString();
                competency.Description = request.Vm.Description;
                competency.IsActive = request.Vm.IsActive;
                competency.IsRejected = false;

                _repo.UpdateRecord(competency);
                message = "Updated successfully";
            }
        }
        else
        {
            competency = new Competency
            {
                CompetencyCategoryId = request.Vm.CompetencyCategoryId,
                CompetencyName = request.Vm.CompetencyName.ToUpperString(),
                Description = request.Vm.Description,
                IsActive = request.Vm.IsActive,
                IsRejected = false,
            };
            await _repo.AddRecord(competency);
            message = $"Created successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Id = competency.CompetencyId.ToString(), Message = result.IsSuccess ? message : "Duplicate competency" };
    }
}

public class ApproveCompetencyHandler : IRequestHandler<ApproveCompetencyCmd, ResponseVm>
{
    private readonly IRepo<Competency> _repo;

    public ApproveCompetencyHandler(IRepo<Competency> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(ApproveCompetencyCmd request, CancellationToken cancellationToken)
    {

        var competency = await _repo.GetById(request.Vm.CompetencyId);
        if (competency != null)
        {
            competency.ApprovedBy = request.Vm.ApprovedBy;
            competency.IsApproved = request.Vm.IsApproved;
            competency.DateApproved = request.Vm.DateApproved;
            competency.IsRejected = false;

            _repo.UpdateRecord(competency);
            var result = await _repo.SaveContextAsync();
            return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? "Approved successfully" : result.Message };
        }
        return new ResponseVm { IsSuccess = false, Message = "Invalid Competency ID" };
    }
}

public class DeleteCompetencyHandler : IRequestHandler<DeleteCompetencyCmd, ResponseVm>
{
    private readonly IRepo<Competency> _repo;

    public DeleteCompetencyHandler(IRepo<Competency> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteCompetencyCmd request, CancellationToken cancellationToken)
    {
        var competency = await _repo.GetById(request.Id);

        if (request.IsSoftDelete && competency != null)
        {
            competency.SoftDeleted = true;
            competency.IsActive = false;
            _repo.UpdateRecord(competency);
        }
        else
        {
            await _repo.Delete(request.Id);
        }

        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"{competency.CompetencyName} Competency has been deleted successfully" : result.Message };
    }
}

public class RejectCompetencyHandler : IRequestHandler<RejectCompetencyCmd, ResponseVm>
{
    private readonly IRepo<Competency> _repo;

    public RejectCompetencyHandler(IRepo<Competency> repo)
    {
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(RejectCompetencyCmd request, CancellationToken cancellationToken)
    {

        var competency = await _repo.GetById(request.Vm.CompetencyId);
        if (competency != null)
        {
            competency.IsApproved = false;
            competency.RejectedBy = request.Vm.RejectedBy;
            competency.DateRejected = request.Vm.DateRejected;
            competency.IsRejected = request.Vm.IsRejected;
            competency.RejectionReason = request.Vm.RejectionReason;

            _repo.UpdateRecord(competency);
            var result = await _repo.SaveContextAsync();
            return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? "Rejected successfully" : result.Message };
        }
        return new ResponseVm { IsSuccess = false, Message = "Invalid Competency ID" };
    }
}