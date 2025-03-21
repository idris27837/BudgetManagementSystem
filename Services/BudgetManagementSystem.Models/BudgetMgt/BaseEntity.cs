using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Audit.EntityFramework;
using Innofactor.EfCoreJsonValueConverter;
using BudgetManagementSystem.Models.AbstractModel;

namespace BudgetManagementSystem.Models.BudgetMgt
{
    public abstract partial class BaseEntity : ISoftDelete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [AuditIgnore]
        public int Id { get; set; }
        [AuditIgnore]
        public Status RecordStatus { get; set; } = PMS.Models.Status.Active;
        [AuditIgnore]
        public DateTime? CreatedAt { get; set; }
        public bool SoftDeleted { get; set; }
        public string Status { get; set; }
        [AuditIgnore]
        public DateTime? UpdatedAt { get; set; }
        [AuditIgnore]
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [AuditIgnore]
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }




    public abstract class BaseWorkFlow : BaseEntity
    {
       
        public string ApprovedBy { get; set; }
        public DateTime DateApproved { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public string RejectedBy { get; set; }
        public string RejectionReason { get; set; }
        public DateTime DateRejected { get; set; }
    }


    public abstract class HrdWorkFlow : BaseWorkFlow
    {
        public string HrdApprovedBy { get; set; }
        public DateTime HrdDateApproved { get; set; }
        public bool HrdIsApproved { get; set; }
        public bool HrdIsRejected { get; set; }
        public string HrdRejectedBy { get; set; }
        public string HrdRejectionReason { get; set; }
        public DateTime HrdDateRejected { get; set; }
    }

}
