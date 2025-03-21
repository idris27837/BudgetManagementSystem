global using BudgetManagementSystem.ViewModels;
global using System.ComponentModel.DataAnnotations;
using BudgetManagementSystem.ViewModels.UserRoleMgtVm;

namespace BudgetManagementSystem.ViewModels;

public class ResponseVm : BaseAPIResponse
{
    public string Id { get; set; }
}
public class ApiErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string TraceId { get; set; }
}
public sealed class EnumList
{
    public string Name { get; set; }
    public int ID { get; set; }
}

public abstract class BaseAuditVm
{
    public bool IsActive { get; set; } = true;
}
public abstract class BasePagedData
{
    public int Skip { get; set; } = 0;
    public int PageSize { get; set; } = 20;
}
public sealed class WebAppAPIConfig
{
    public string BaseUrl { get; set; }
    public string APIKey { get; set; }
    public string Url { get; set; }
}
public class EmailRequest
{
    public string UserId { get; set; }
    public string Title { get; set; }
    public int RecordCount { get; set; } = 0;
    public string EmailDescription { get; set; }
}

public class SoaJobRoleVm
{
    public int P_PERSON_ID { get; set; }
    public string P_JOB_ROLE { get; set; }

}
public class SoaOutputParameters : BaseAPIResponse
{
    public string UPDATE_EMPLOYEE_JOB_ROLE { get; set; }

}
