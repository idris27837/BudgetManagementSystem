namespace OfficeMgt.BusinessLogic.Handlers.OrganogramHandlers;

/// <summary>
/// Class AddOrUpdatOfficeHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.SaveOfficeCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.SaveOfficeCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// <remarks>
/// Initializes a new instance of the <see cref="AddOrUpdatOfficeHandler"/> class.
/// </remarks>
/// <param name="repo">The repo.</param>
public class AddOrUpdateOfficeHandler(IRepo<Office> repo) : IRequestHandler<SaveOfficeCmd, ResponseVm>
{

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async ValueTask<ResponseVm> Handle(SaveOfficeCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.OfficeId > 0)
        {
            var office = await repo.GetById(request.Vm.OfficeId);
            if (office != null)
            {
                office.OfficeCode = request.Vm.OfficeCode.ToUpperString();
                office.OfficeName = request.Vm.OfficeName;
                office.DivisionId = request.Vm.DivisionId;
                office.IsActive = request.Vm.IsActive;

                repo.UpdateRecord(office);
                message = $"{office.OfficeName} Office has been Updated Successfully";
            }
        }
        else
        {
            var model = new Office
            {
                OfficeCode = request.Vm.OfficeCode.ToUpperString(),
                OfficeName = request.Vm.OfficeName,
                DivisionId = request.Vm.DivisionId,
                IsActive = request.Vm.IsActive
            };
            await repo.AddRecord(model);
            message = $"{model.OfficeName} Office has been Created Successfully";
        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? message : result.Message };
    }
}

/// <summary>
/// Class DeleteOfficeHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.DeleteOfficeCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.DeleteOfficeCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// <remarks>
/// Initializes a new instance of the <see cref="DeleteOfficeHandler"/> class.
/// </remarks>
/// <param name="repo">The repo.</param>
public class DeleteOfficeHandler(IRepo<Office> repo) : IRequestHandler<DeleteOfficeCmd, ResponseVm>
{

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async ValueTask<ResponseVm> Handle(DeleteOfficeCmd request, CancellationToken cancellationToken)
    {
        var office = await repo.GetById(request.Id);

        if (request.IsSoftDelete && office != null)
        {
            office.SoftDeleted = true;
            office.IsActive = false;
            repo.UpdateRecord(office);
        }
        else
        {
            await repo.Delete(request.Id);
        }

        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"{office.OfficeName} Office has been deleted successfully" : result.Message };
    }
}

/// <summary>
/// Class GetOfficeQueryHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Queries.GetOfficeQuery, System.Collections.Generic.List{OfficeMgt.ViewModels.OrganogramVm.OfficeVm}}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Queries.GetOfficeQuery, System.Collections.Generic.List{OfficeMgt.ViewModels.OrganogramVm.OfficeVm}}" />
/// <remarks>
/// Initializes a new instance of the <see cref="GetOfficeQueryHandler"/> class.
/// </remarks>
/// <param name="repo">The repo.</param>
public class GetOfficeQueryHandler(IRepo<Office> repo) : IRequestHandler<GetOfficeQuery, List<OfficeVm>>
{

    /// <summary>
    /// Handles the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>List&lt;OfficeVm&gt;.</returns>
    public async ValueTask<List<OfficeVm>> Handle(GetOfficeQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Office> query;

        query = repo.GetAllByQuery(null);

        if (request.DivisionId.HasValue)
        {
            query = repo.GetAllByQuery(x => x.DivisionId.Equals((int)request.DivisionId));
        }

        query = query.Include(i => i.Division);

        return await query.Select(s => new OfficeVm
        {
            OfficeId = s.OfficeId,
            OfficeCode = s.OfficeCode,
            OfficeName = s.OfficeName,
            DivisionName = s.Division.DivisionName,
            DivisionId = s.DivisionId,
        }).ToListAsync(cancellationToken);
    }
}
public class GetOfficeByIdQueryHandler(IRepo<Office> repo) : IRequestHandler<GetOfficeByIdQuery, OfficeVm>
{
    public async ValueTask<OfficeVm> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Office> query;


        query = repo.GetAllByQuery(x => x.OfficeCode.Equals(request.OfficeId));

        query = query.Include(i => i.Division);

        return await query.Select(s => new OfficeVm
        {
            OfficeId = s.OfficeId,
            OfficeCode = s.OfficeCode,
            OfficeName = s.OfficeName,
            DivisionName = s.Division.DivisionName,
            DivisionId = s.DivisionId,
        }).FirstOrDefaultAsync(cancellationToken);
    }
}