﻿namespace BudgetManagementSystem.Models.Core
{
    public class Permission : BaseAudit
    {

        public int PermissionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }

    }
}