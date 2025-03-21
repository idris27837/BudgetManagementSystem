using BudgetManagementSystem.Models.Core;
using Microsoft.AspNetCore.Identity;

namespace BudgetManagementSystem.BusinessLogic.Handlers.StaffMgtHandlers;


public class GetStaffsQueryHandler : IRequestHandler<GetStaffsQuery, List<ApplicationUser>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetStaffsQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async ValueTask<List<ApplicationUser>> Handle(GetStaffsQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.searchSting))
        {
            return await _userManager.Users.Where(x => x.FirstName.ToLower().Contains(request.searchSting.ToLower()) ||
                x.LastName.ToLower().Contains(request.searchSting.ToLower()) || x.UserName.ToLower().Contains(request.searchSting.ToLower())
                || x.Email.ToLower().Contains(request.searchSting.ToLower())).ToListAsync();

        }
        else
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}

public class GetStaffRoleHandler : IRequestHandler<GetStaffRoleQuery, List<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetStaffRoleHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async ValueTask<List<string>> Handle(GetStaffRoleQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.StaffId);

        var staffRoles = user is null ? new List<string>() : await _userManager.GetRolesAsync(user);
        return staffRoles.ToList();
    }
}

public class CreateStaffHandler : IRequestHandler<CreateStaffCmd, ResponseVm>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateStaffHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async ValueTask<ResponseVm> Handle(CreateStaffCmd request, CancellationToken cancellationToken)
    {
        var result = new IdentityResult();
        var user = await _userManager.FindByIdAsync(request.Vm.Id);
        if (user is null)
        {

            result = await _userManager.CreateAsync(new ApplicationUser
            {
                Id = request.Vm.Id,
                UserName = request.Vm.Email,
                Email = request.Vm.Email,
                FirstName = request.Vm.FirstName,
                LastName = request.Vm.LastName,
                IsActive = request.Vm.IsActive
            });
        }
        else
        {
            user.IsActive = request.Vm.IsActive;
            result = await _userManager.UpdateAsync(user);
        }

        var msg = "";
        if (result.Succeeded)
        {
            msg = "Operation completed successfully";
        }
        else
        {
            if (result.Errors.Any())
            {
                msg = string.Join(",", result.Errors.Select(s => s.Description));
            }
            else
            {
                msg = "Process could not be completed, please try again";
            }
        }

        return new ResponseVm
        {
            IsSuccess = result.Succeeded,
            Message = msg
        };
    }
}

public class AddStaffToRoleHandler : IRequestHandler<AddStaffToRoleCmd, ResponseVm>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AddStaffToRoleHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async ValueTask<ResponseVm> Handle(AddStaffToRoleCmd request, CancellationToken cancellationToken)
    {

        var user = await _userManager.FindByIdAsync(request.Vm.UserId);
        if (user is null)
        {
            var createResult = await _userManager.CreateAsync(new ApplicationUser
            {
                Id = request.Vm.UserId,
                UserName = request.Vm.Email,
                Email = request.Vm.Email,
                FirstName = request.Vm.FirstName,
                LastName = request.Vm.LastName
            });
        }
        var result = await _userManager.AddToRoleAsync(await _userManager.FindByIdAsync(request.Vm.UserId), request.Vm.RoleName);
        var userRoles = await _userManager.GetRolesAsync(user);
        if (result.Succeeded)
        {
            return new ResponseVm { IsSuccess = result.Succeeded, Message = $"User Added to {request.Vm.RoleName} Role Successfully" };
        }
        return new ResponseVm
        {
            IsSuccess = false,
            Message = userRoles is not null && userRoles.Any(x => x.ToUpper().Equals(request.Vm.RoleName.ToUpper())) ?
                    $"User already in {request.Vm.RoleName} role" : "Something went wrong"
        };
    }
}

public class DeleteStaffFromRoleHandler : IRequestHandler<DeleteStaffFromRoleCmd, ResponseVm>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public DeleteStaffFromRoleHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async ValueTask<ResponseVm> Handle(DeleteStaffFromRoleCmd request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);
        if (result.Succeeded)
        {
            return new ResponseVm { IsSuccess = result.Succeeded, Message = $"User Removed from {request.RoleName} Role Successfully" };
        }
        return new ResponseVm { IsSuccess = false, Message = "Something went wrong" };
    }
}



