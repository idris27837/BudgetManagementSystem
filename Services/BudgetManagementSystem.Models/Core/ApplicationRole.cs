using Microsoft.AspNetCore.Identity;

namespace BudgetManagementSystem.Models.Core
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
        public ICollection<RolePermission> RolePermissions { get; set; }

    }
}
