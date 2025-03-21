
namespace BudgetManagementSystem.ViewModels.UserRoleMgtVm
{
    public class PermissionVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class RolePermissionVm
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<PermissionVm> Permissions { get; set; } = new List<PermissionVm>();
    }

    public class GetRolePermissionVm
    {
        public List<PermissionVm> AllPermissions { get; set; } = new List<PermissionVm>();
        public RolePermissionVm RolesAndPermissions { get; set; }
    }

    public class AddPermissionToRoleVm
    {
        public string RoleId { get; set; }
        public int PermissionId { get; set; }
    }
}
