
namespace OfficeMgt.BusinessLogic.Handlers.OrganogramHandlers;

/// <summary>
/// Class SaveDepartmentHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.SaveDepartmentCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.SaveDepartmentCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// <remarks>
/// Initializes a new instance of the <see cref="SaveDepartmentHandler"/> class.
/// </remarks>
/// <param name="repo">The repo.</param>
public class SaveDepartmentHandler(IRepo<Department> repo) : IRequestHandler<SaveDepartmentCmd, ResponseVm>
{

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async ValueTask<ResponseVm> Handle(SaveDepartmentCmd request, CancellationToken cancellationToken)
    {
        string message = string.Empty;
        if (request.Vm.DepartmentId > 0)
        {
            var dept = await repo.GetById(request.Vm.DepartmentId);
            if (dept != null)
            {
                dept.DepartmentCode = request.Vm.DepartmentCode.ToUpperString();
                dept.DepartmentName = request.Vm.DepartmentName;
                dept.DirectorateId = request.Vm.DirectorateId;
                dept.IsBranch = request.Vm.IsBranch;

                repo.UpdateRecord(dept);
                message = $"{dept.DepartmentName} Department has been Updated Successfully";
            }
        }
        else
        {
            var model = new Department
            {
                DepartmentCode = request.Vm.DepartmentCode.ToUpperString(),
                DepartmentName = request.Vm.DepartmentName,
                DirectorateId = request.Vm.DirectorateId,
                IsBranch = request.Vm.IsBranch,
                IsActive = request.Vm.IsActive
            };
            await repo.AddRecord(model);
            message = $"{model.DepartmentName} Department has been Created Successfully";
        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm
        {
            IsSuccess = result.IsSuccess,
            Message = result.IsSuccess ? message : result.Message
        };
    }
}

/// <summary>
/// Class DeleteDepartmentHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.DeleteDepartmentCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Commands.OrganogramCmd.DeleteDepartmentCmd, OfficeMgt.ViewModels.ResponseVm}" />
/// <remarks>
/// Initializes a new instance of the <see cref="DeleteDepartmentHandler"/> class.
/// </remarks>
/// <param name="repo">The repo.</param>
public class DeleteDepartmentHandler(IRepo<Department> repo) : IRequestHandler<DeleteDepartmentCmd, ResponseVm>
{

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="request">The request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async ValueTask<ResponseVm> Handle(DeleteDepartmentCmd request, CancellationToken cancellationToken)
    {
        var department = await repo.GetById(request.Id);

        if (request.IsSoftDelete && department != null)
        {
            department.SoftDeleted = true;
            department.IsActive = false;
            repo.UpdateRecord(department);
        }
        else
        {
            await repo.Delete(request.Id);
        }

        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? $"{department.DepartmentName} Department has been deleted successfully" : result.Message };
    }
}

/// <summary>
/// Class GetDepartmentQueryHandler.
/// Implements the <see cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Queries.GetDepartmentQuery, System.Collections.Generic.List{OfficeMgt.ViewModels.OrganogramVm.DepartmentVm}}" />
/// </summary>
/// <seealso cref="MediatR.IRequestHandler{OfficeMgt.BusinessLogic.Queries.GetDepartmentQuery, System.Collections.Generic.List{OfficeMgt.ViewModels.OrganogramVm.DepartmentVm}}" />
/// <remarks>
/// Initializes a new instance of the <see cref="GetDepartmentQueryHandler"/> class.
/// </remarks>
/// <param name="repo">The repo.</param>
public class GetDepartmentQueryHandler(IRepo<Department> repo) : IRequestHandler<GetDepartmentQuery, List<DepartmentVm>>
{

    /// <summary>
    /// Handles the specified request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>List&lt;DepartmentVm&gt;.</returns>
    public async ValueTask<List<DepartmentVm>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Department> query;

        query = repo.GetAllByQuery(null).Include(i => i.Directorate);

        if (request.DirectorateId.HasValue)
        {
            query = repo.GetAllByQuery(x => x.DirectorateId.Equals((int)request.DirectorateId));
        }

        query = query.Include(i => i.Directorate);

        return await query.Select(s => new DepartmentVm
        {
            DepartmentId = s.DepartmentId,
            DepartmentName = s.DepartmentName,
            DepartmentCode = s.DepartmentCode,
            DirectorateId = s.DirectorateId,
            DirectorateName = s.Directorate.DirectorateName,
            IsBranch = s.IsBranch,
        }).ToListAsync(cancellationToken);
    }
}
