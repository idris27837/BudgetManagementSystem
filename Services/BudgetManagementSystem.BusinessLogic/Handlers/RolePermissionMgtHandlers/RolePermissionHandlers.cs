using BudgetManagementSystem.Models.Core;
using BudgetManagementSystem.ViewModels.UserRoleMgtVm;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace BudgetManagementSystem.BusinessLogic.Handlers.StaffMgtHandlers;


public class GetRolePermissionQueryHandler(IRepo<Permission> repo, RoleManager<ApplicationRole> roleManager) : IRequestHandler<GetRolePermissionsQuery, GetRolePermissionVm>
{
    public async ValueTask<GetRolePermissionVm> Handle(GetRolePermissionsQuery request, CancellationToken cancellationToken)
    {
        var vm = new GetRolePermissionVm();

        IQueryable<Permission> query = repo.GetAllByQuery(null);

        var role = await roleManager.Roles?.Where(r => r.Id == request.roleId)
                                             .Include(r => r.RolePermissions)
                                              .ThenInclude(p => p.Permission).FirstOrDefaultAsync(cancellationToken);

        vm.AllPermissions = await query.Select(p => new PermissionVm
        {
            Id = p.PermissionId,
            Name = p.Name,
            Description = p.Description,
        }).ToListAsync(cancellationToken);

        if (role is not null)
        {
            vm.RolesAndPermissions = new RolePermissionVm
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Permissions = role.RolePermissions.Select(p => new PermissionVm
                {
                    Id = p.Permission.PermissionId,
                    Name = p.Permission.Name,
                    Description = p.Permission.Description
                }).ToList()
            };
        }

        return vm;
    }
}

public class GetPermissionsByRoleQueryQueryHandler(RoleManager<ApplicationRole> roleManager, IRepo<Permission> repo) : IRequestHandler<GetPermissionsByRoleQuery, List<PermissionVm>>
{

    public async ValueTask<List<PermissionVm>> Handle(GetPermissionsByRoleQuery request, CancellationToken cancellationToken)
    {

        var permissions = new List<PermissionVm>();

        if (!string.IsNullOrEmpty(request.roleId))
        {
            var role = await roleManager.Roles.Include(r => r.RolePermissions)
                                               .ThenInclude(p => p.Permission)
                                               .Where(r => r.Id == request.roleId).FirstOrDefaultAsync(cancellationToken);
            if (role is not null)
                permissions = role.RolePermissions.Select(p => new PermissionVm
                {
                    Id = p.Permission.PermissionId,
                    Name = p.Permission.Name,
                    Description = p.Permission.Description
                }).ToList();
        }

        else
        {
            var query = repo.GetAllByQuery(null);
            permissions = await query.Select(p => new PermissionVm
            {
                Id = p.PermissionId,
                Name = p.Name,
                Description = p.Description
            }).ToListAsync(cancellationToken);
        }

        return permissions;
    }
}

public class AddPermissionToRoleHandler(IRepo<RolePermission> repo) : IRequestHandler<AddPermissionToRoleCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(AddPermissionToRoleCmd request, CancellationToken cancellationToken)
    {
        var rolePermission = await repo.GetAllByQuery(r => r.RoleId == request.Vm.RoleId && r.PermissionId == request.Vm.PermissionId).FirstOrDefaultAsync(cancellationToken);
        if (rolePermission != null)
            return new ResponseVm { IsSuccess = false, Message = "Permission already assigned to role" };

        else
        {
            await repo.AddRecord(new RolePermission
            {
                RoleId = request.Vm.RoleId,
                PermissionId = request.Vm.PermissionId,
                DateCreated = DateTime.Now,
                SoftDeleted = false
            });

        }
        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? "Permission assigned successfully" : result.Message };
    }
}

public class RemovePermissionFromRoleCmdHandler(IRepo<RolePermission> repo) : IRequestHandler<RemovePermissionFromRoleCmd, ResponseVm>
{
    public async ValueTask<ResponseVm> Handle(RemovePermissionFromRoleCmd request, CancellationToken cancellationToken)
    {
        var rolePermission = await repo.GetAllByQuery(r => r.RoleId == request.roleId && r.PermissionId == request.permissionId).FirstOrDefaultAsync(cancellationToken);
        if (rolePermission is null)
            return new ResponseVm { IsSuccess = false, Message = "Permission to remove was not previously assigned to role" };

        await repo.DeleteAsync(p => p.RoleId == request.roleId && p.PermissionId == request.permissionId);
        var result = await repo.SaveContextAsync();
        return new ResponseVm { IsSuccess = result.IsSuccess, Message = result.IsSuccess ? "Permission removed successfully" : result.Message };
    }
}


