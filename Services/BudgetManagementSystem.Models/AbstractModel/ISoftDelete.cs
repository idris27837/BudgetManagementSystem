namespace BudgetManagementSystem.Models.AbstractModel;

public interface ISoftDelete
{
    bool SoftDeleted { get; set; }
}
