global using BudgetManagementSystem.Models.AbstractModel;
global using BudgetManagementSystem.Models.Core;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManagementSystem.Models.AbstractModel;

public abstract class BaseAudit : ISoftDelete
{
    [StringLength(75)]
    public string CreatedBy { get; set; } = "SYSTEM";
    public DateTime DateCreated { get; set; }
    public bool IsActive { get; set; } = true;

    [StringLength(25)]
    public string Status { get; set; }
    public bool SoftDeleted { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string UpdatedBy { get; set; }
}



public abstract class BaseWorkFlowData : BaseAudit
{
    public string ApprovedBy { get; set; }
    public DateTime DateApproved { get; set; }
    public bool IsApproved { get; set; }
    public bool IsRejected { get; set; }
    public string RejectedBy { get; set; }
    public string RejectionReason { get; set; }
    public DateTime DateRejected { get; set; }
}


public abstract class HrdWorkFlowData : BaseWorkFlowData
{
    public string HrdApprovedBy { get; set; }
    public DateTime HrdDateApproved { get; set; }
    public bool HrdIsApproved { get; set; }
    public bool HrdIsRejected { get; set; }
    public string HrdRejectedBy { get; set; }
    public string HrdRejectionReason { get; set; }
    public DateTime HrdDateRejected { get; set; }
}
