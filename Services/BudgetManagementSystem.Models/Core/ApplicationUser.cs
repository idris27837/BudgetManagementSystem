using Microsoft.AspNetCore.Identity;

namespace BudgetManagementSystem.Models.Core;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsActive { get; set; }
    public string FullName => $"{FirstName} {LastName}";
}
