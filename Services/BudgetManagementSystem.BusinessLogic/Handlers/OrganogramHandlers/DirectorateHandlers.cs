
namespace OfficeMgt.BusinessLogic.Handlers.OrganogramHandlers;

/// <summary>
/// Class SaveDirectorateHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.SaveDirectorateCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.SaveDirectorateCmd, OfficeMgt.ViewModels.ResponseVm}" />
public class SaveDirectorateHandler : IRequestHandler<SaveDirectorateCmd, ResponseVm>
{
    /// <summary>
    /// The repo
    /// </summary>
    private readonly IRepo<Directorate> _repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaveDirectorateHandler"/> class.
    /// </summary>
    /// <param name="repo">The repo.</param>
    public SaveDirectorateHandler(IRepo<Directorate> repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async ValueTask<ResponseVm> Handle(SaveDirectorateCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.DirectorateId > 0)
        {
            var ward = await _repo.GetById(request.Vm.DirectorateId);
            if (ward != null)
            {
                ward.DirectorateCode = request.Vm.DirectorateCode.ToUpperString();
                ward.DirectorateName = request.Vm.DirectorateName;

                _repo.UpdateRecord(ward);
                message = $"{ward.DirectorateName} Directorate has been Updated Successfully";
            }
        }
        else
        {
            var model = new Directorate
            {
                DirectorateCode = request.Vm.DirectorateCode.ToUpperString(),
                DirectorateName = request.Vm.DirectorateName
            };
            await _repo.AddRecord(model);
            message = $"{model.DirectorateName} Directorate has been Created Successfully";
        }
        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

/// <summary>
/// Class DeleteDirectorateHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.DeleteDirectorateCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.DeleteDirectorateCmd, OfficeMgt.ViewModels.ResponseVm}" />
public class DeleteDirectorateHandler : IRequestHandler<DeleteDirectorateCmd, ResponseVm>
{
    /// <summary>
    /// The repo
    /// </summary>
    private readonly IRepo<Directorate> _repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDirectorateHandler"/> class.
    /// </summary>
    /// <param name="repo">The repo.</param>
    public DeleteDirectorateHandler(IRepo<Directorate> repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async ValueTask<ResponseVm> Handle(DeleteDirectorateCmd request, CancellationToken cancellationToken)
    {
        var directorate = await _repo.GetById(request.Id);

        if (request.IsSoftDelete && directorate != null)
        {
            directorate.SoftDeleted = true;
            directorate.IsActive = false;
            _repo.UpdateRecord(directorate);
        }
        else
        {
            await _repo.Delete(request.Id);
        }

        var result = await _repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"{directorate.DirectorateName} Directorate has been deleted successfully" : result.Message };
    }
}

/// <summary>
/// Class GetDirectorateQueryHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Queries.GetDirectorateQuery, System.Collections.Generic.List{OfficeMgt.ViewModels.OrganogramVm.DirectorateVm}}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Queries.GetDirectorateQuery, System.Collections.Generic.List{OfficeMgt.ViewModels.OrganogramVm.DirectorateVm}}" />
public class GetDirectorateQueryHandler : IRequestHandler<GetDirectorateQuery, List<DirectorateVm>>
{
    /// <summary>
    /// The repo
    /// </summary>
    private readonly IRepo<Directorate> _repo;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDirectorateQueryHandler"/> class.
    /// </summary>
    /// <param name="repo">The repo.</param>
    public GetDirectorateQueryHandler(IRepo<Directorate> repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Handles the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>List&lt;DirectorateVm&gt;.</returns>
    public async ValueTask<List<DirectorateVm>> Handle(GetDirectorateQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Directorate> query;

        query = _repo.GetAllByQuery(null);

        return await query.Select(s => new DirectorateVm
        {
            DirectorateId = s.DirectorateId,
            DirectorateCode = s.DirectorateCode,
            DirectorateName = s.DirectorateName,
        }).ToListAsync();
    }
}
