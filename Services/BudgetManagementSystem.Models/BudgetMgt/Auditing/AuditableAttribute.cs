using BudgetManagementSystem.Models.BudgetMgt;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManagementSystem.Models.Auditing
{
    [Table("AuditableAttribute", Schema = "pmsaudit")]
    public class AuditableAttribute : BaseEntity
    {
        //[NotMapped]
        //public int AuditableAttributeId { get; set; }
        public int AuditableEntityId { get; set; }
        public string AttributeName { get; set; }
        public bool EnableAudit { get; set; }
        //public override int Id
        //{
        //    get { return AuditableAttributeId; }
        //    set { AuditableAttributeId = value; }
        //}
        public virtual AuditableEntity AuditableEntity { get; set; }
    }
}
