namespace BudgetManagementSystem.Models.AbstractModel;
public abstract class Person : BaseAudit
{
    [StringLength(150)]
    public string FirstName { get; set; }

    [StringLength(150)]
    public string LastName { get; set; }

    [StringLength(150)]
    public string MiddleName { get; set; }

    [StringLength(15)]
    public string PhoneNumber { get; set; }

    [UniqueKey(groupId: "2", order: 0)]
    [StringLength(75, MinimumLength = 1)]
    public string Email { get; set; }

    public byte[] Passport { get; set; }

    [NotMapped]
    public string FullName => $"{LastName}, {FirstName} {MiddleName}";
}
