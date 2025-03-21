using BudgetManagementSystem.Models.Constants;
using BudgetManagementSystem.ViewModels.DTOs;
using BudgetManagementSystem.ViewModels.UserRoleMgtVm;
using Microsoft.Extensions.Logging;
using System.DirectoryServices;
using System.Runtime.Versioning;
using DirectoryEntry = System.DirectoryServices.DirectoryEntry;
using Encoder = Microsoft.Security.Application.Encoder;

namespace BudgetManagementSystem.Infrastructure.Concrete;

[SupportedOSPlatform("windows")]
public sealed class ActiveDirectoryService(IOptions<ActiveDirectoryConfig> activeDirectoryConfig, ILogger<ActiveDirectoryService> logger = null)
{

    private readonly ActiveDirectoryConfig _activeDirectoryConfig = activeDirectoryConfig.Value;


    /// <summary>
    /// Logins to ad.
    /// </summary>
    /// <param name="userName">The userName.</param>
    /// <param name="password">The password.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public ActiveDirectoryLoginResponseVm LoginToAD(string userName, string password, string domain)
    {
        string _domainUsername = $"{domain}" + "\\" + userName;
        var entry = new DirectoryEntry(_activeDirectoryConfig.Url, _domainUsername, password);
        string safeUserName = Encoder.LdapFilterEncode(userName); 
        try
        {
            var search = new DirectorySearcher(entry)
            {
                Filter = $"(SAMAccountName={safeUserName})"
            };
            var result = search.FindOne();
            if (result == null)
            {
                logger.LogWarning("Logging failed");
                return new ActiveDirectoryLoginResponseVm { IsSuccess = false, Message = "User not found in AD" };
            }
            else
            {
                var adObject = result.Properties;
                var adUser = new ADUser
                {
                    FullName = (string)((adObject["name"].Count > 0) ? adObject["name"][0] : ""),
                    Department = (string)((adObject["department"].Count > 0) ? adObject["department"][0] : ""),
                    EmployeeId = (string)((adObject["employeeid"].Count > 0) ? adObject["employeeid"][0] : ""),
                    Mail = (string)((adObject["mail"].Count > 0) ? adObject["mail"][0] : ""),
                    Phone = (string)((adObject["telephonenumber"].Count > 0) ? adObject["telephonenumber"][0] : ""),
                    Title = (string)((adObject["title"].Count > 0) ? adObject["title"][0] : ""),
                    UserName = (string)((adObject["samaccountname"].Count > 0) ? adObject["samaccountname"][0] : ""),
                    OrganizationUnit = (string)((adObject["ou"].Count > 0) ? adObject["ou"][0] : "")
                };
                logger.LogInformation("Logging successful");

                return new ActiveDirectoryLoginResponseVm { IsSuccess = true, Message = "Login successful", ADUser = adUser };
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning("Logging failed because {message}", ex.Message);
            return new ActiveDirectoryLoginResponseVm { IsSuccess = false, Message = ex.Message };
        }
    }

    public List<string> LoggedInUserRoles(EmployeeErpDetailsDTO employee)
    {
        var roles = new List<string> { RoleName.Staff };
        if (employee.EmployeeNumber.Equals(employee.HeadOfOfficeId) && !roles.Contains(RoleName.HeadOfOffice))
        {
            roles.Add(RoleName.HeadOfOffice);
        }
        if (employee.EmployeeNumber.Equals(employee.HeadOfDivId) && !roles.Contains(RoleName.HeadOfDivision))
        {
            roles.Add(RoleName.HeadOfDivision);
        }
        if (employee.EmployeeNumber.Equals(employee.HeadOfDeptId) && (!roles.Contains(RoleName.HeadOfDepartment) || !roles.Contains(RoleName.Director)))
        {
            roles.Add(RoleName.HeadOfDepartment);
            roles.Add(RoleName.Director);
        }

        return roles;
    }
}
