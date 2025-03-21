using Audit.EntityFramework;
using BudgetManagementSystem.Models.BudgetMgt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
#nullable disable
namespace BudgetManagementSystem.Models.Core
{
    [AuditInclude]
    [Table("PmsConfigurations", Schema = "pms")]
    [PrimaryKey("PmsConfigurationId")]
    public class PmsConfiguration : BaseEntity {
        public string PmsConfigurationId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsEncrypted { get; set; } = false;
    }
}
