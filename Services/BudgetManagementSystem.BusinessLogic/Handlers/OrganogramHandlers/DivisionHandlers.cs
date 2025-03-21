namespace OfficeMgt.BusinessLogic.Handlers.OrganogramHandlers;

/// <summary>
/// Class AddOrUpdatDivisionHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.SaveDivisionCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.SaveDivisionCmd, OfficeMgt.ViewModels.ResponseVm}" />
public class AddOrUpdatDivisionHandler : IRequestHandler<SaveDivisionCmd, ResponseVm>
{
    /// <summary>
    /// The repo
    /// </summary>
    private readonly IRepo<Division> _repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddOrUpdatDivisionHandler"/> class.
    /// </summary>
    /// <param name="repo">The repo.</param>
    /// <param name="mapper">The mapper.</param>
    public AddOrUpdatDivisionHandler(IRepo<Division> repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async ValueTask<ResponseVm> Handle(SaveDivisionCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.DivisionId > 0)
        {
            var division = await _repo.GetById(request.Vm.DepartmentId);
            if (division != null)
            {
                division.DivisionCode = request.Vm.DivisionCode.ToUpperString();
                division.DivisionName = request.Vm.DivisionName;
                division.DepartmentId = request.Vm.DepartmentId;
                division.IsActive = request.Vm.IsActive;

                _repo.UpdateRecord(division);
                message = $"{division.DivisionName} Division has been Updated Successfully";
            }
        }
        else
        {
            var model = new Division
            {
                DivisionCode = request.Vm.DivisionCode.ToUpperString(),
                DivisionName = request.Vm.DivisionName,
                DepartmentId = request.Vm.DepartmentId,
                IsActive = request.Vm.IsActive,
            };
            await _repo.AddRecord(model);
            message = $"{model.DivisionName} Division has been Created Successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

/// <summary>
/// Class DeleteDivisionHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.DeleteDivisionCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.DeleteDivisionCmd, OfficeMgt.ViewModels.ResponseVm}" />
public class DeleteDivisionHandler : IRequestHandler<DeleteDivisionCmd, ResponseVm>
{
    /// <summary>
    /// The repo
    /// </summary>
    private readonly IRepo<Division> _repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDivisionHandler"/> class.
    /// </summary>
    /// <param name="repo">The repo.</param>
    public DeleteDivisionHandler(IRepo<Division> repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async ValueTask<ResponseVm> Handle(DeleteDivisionCmd request, CancellationToken cancellationToken)
    {
        var division = await _repo.GetById(request.Id);

        if (request.IsSoftDelete && division != null)
        {
            division.SoftDeleted = true;
            division.IsActive = false;
            _repo.UpdateRecord(division);
        }
        else
        {
            await _repo.Delete(request.Id);
        }

        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"{division.DivisionName} Division has been deleted successfully" : result.Message };
    }
}

/// <summary>
/// Class GetDivisionQueryHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Queries.GetDivisionQuery, System.Collections.Generic.List{OfficeMgt.ViewModels.OrganogramVm.DivisionVm}}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Queries.GetDivisionQuery, System.Collections.Generic.List{OfficeMgt.ViewModels.OrganogramVm.DivisionVm}}" />
public class GetDivisionQueryHandler : IRequestHandler<GetDivisionQuery, List<DivisionVm>>
{
    /// <summary>
    /// The repo
    /// </summary>
    private readonly IRepo<Division> _repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDivisionQueryHandler"/> class.
    /// </summary>
    /// <param name="repo">The repo.</param>
    public GetDivisionQueryHandler(IRepo<Division> repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Handles the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>List&lt;DivisionVm&gt;.</returns>
    public async ValueTask<List<DivisionVm>> Handle(GetDivisionQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Division> query;

        query = _repo.GetAllByQuery(null);

        if (request.DepartmentId.HasValue)
        {
            query = _repo.GetAllByQuery(x => x.DepartmentId.Equals((int)request.DepartmentId));
        }

        query = query.Include(i => i.Department);

        return await query.Select(s => new DivisionVm
        {
            DivisionId = s.DivisionId,
            DivisionName = s.DivisionName,
            DivisionCode = s.DivisionCode,
            DepartmentId = s.DepartmentId,
            DepartmentName = s.Department.DepartmentName,

        }).ToListAsync();
    }
}