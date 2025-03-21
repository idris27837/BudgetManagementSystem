using BudgetManagementSystem.Models.Constants;
using BudgetManagementSystem.Models.Core;
using BudgetManagementSystem.ViewModels.StaffMgtVm;
using Microsoft.AspNetCore.Identity;

namespace BudgetManagementSystem.BusinessLogic.Handlers.StaffMgtHandlers;

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, List<RoleVm>>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public GetRoleQueryHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async ValueTask<List<RoleVm>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var query = await _roleManager.Roles?.ToListAsync();
        return query.Where(x => x.Name.ToUpper() != RoleName.SuperAdmin.ToUpper()).Select(s => new RoleVm
        {
            Id = s.Id,
            RoleName = s.NormalizedName
        }).ToList();
    }
}

public class SaveRoleHandler : IRequestHandler<SaveRoleCmd, ResponseVm>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public SaveRoleHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async ValueTask<ResponseVm> Handle(SaveRoleCmd request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.Vm.Id))
        {

            var roles = await _roleManager.Roles.ToListAsync();
            var role = roles.FirstOrDefault(x => x.Id.Equals(request.Vm.Id));
            role.Name = request.Vm.RoleName;
            role.NormalizedName = request.Vm.RoleName.ToUpper();
            var result = await _roleManager.UpdateAsync(role);
            return new ResponseVm { IsSuccess = result.Succeeded, Message = result.Succeeded ? "Role Updated Successfully" : "Something went wrong" };
        }
        else
        {
            if (await _roleManager.RoleExistsAsync(request.Vm.RoleName))
            {
                return new ResponseVm { IsSuccess = false, Message = "Role already exist" };
            }
            var result = await _roleManager.CreateAsync(new ApplicationRole { Id = Guid.NewGuid().ToString(), Name = request.Vm.RoleName });
            return new ResponseVm { IsSuccess = result.Succeeded, Message = result.Succeeded ? "Role Created Successfully" : "Something went wrong" };
        }
    }
}

public class DeleteRoleHandler : IRequestHandler<DeleteRoleCmd, ResponseVm>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IRepo<RolePermission> _repo;

    public DeleteRoleHandler(RoleManager<ApplicationRole> roleManager, IRepo<RolePermission> repo)
    {
        _roleManager = roleManager;
        _repo = repo;
    }

    public async ValueTask<ResponseVm> Handle(DeleteRoleCmd request, CancellationToken cancellationToken)
    {

        var roles = await _roleManager.Roles?.ToListAsync();
        IdentityResult result = new();
        if (roles != null && roles.Any(x => x.NormalizedName.Trim().Equals(request.RoleName.ToUpper().Trim())))
        {
            var role = roles.FirstOrDefault(x => x.NormalizedName.Equals(request.RoleName));

            await _repo.DeleteAsync(permissions => permissions.RoleId == role.Id);
            result = await _roleManager.DeleteAsync(role);
        }
        return new ResponseVm { IsSuccess = result.Succeeded, Message = result.Succeeded ? "Role Deleted Successfully" : "Something went wrong" };
    }
}

