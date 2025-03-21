// Ignore Spelling: Admin

namespace BudgetManagementSystem.Models.Constants;

public static class RoleName
{
    public const string SuperAdmin = "SuperAdmin";
    public const string Admin = "Administrator";
    public const string Staff = "Staff";
    public const string HeadOfOffice = "HeadofOffice";
    public const string HeadOfDivision = "HeadOfDivision";
    public const string HeadOfDepartment = "HeadOfDepartment";
    public const string DeputyDirector = "DeputyDirector";
    public const string Director = "Director";
    public const string HrAdmin = "HrAdmin";
    public const string HrApprover = "HrApprover";
    public const string HrReportAdmin = "HrReportAdmin";
    public const string GeneralReportAdmin = "GeneralReportAdmin";
    public const string Supervisor = "Supervisor";
    public const string Smd = "Smd";
    public const string SmdApprover = "SmdApprover";
    public const string SmdOutcomeEvaluator = "SmdOutcomeEvaluator";
    public const string SecurityAdmin = "SecurityAdmin";


    public static List<string> GetRoleList()
    {
        return
        [
            Admin,
            SuperAdmin,
            Staff,
            HeadOfOffice,
            HeadOfDivision,
            HeadOfDepartment,
            DeputyDirector,
            Director,
            HrAdmin,
            HrApprover,
            HrReportAdmin,
            GeneralReportAdmin,
            Smd,
            SmdApprover,
            SmdOutcomeEvaluator,
            SecurityAdmin
        ];
    }

    public static List<string> GetStaffRoles()
    {
        return
        [
            Admin,
            Staff,
            HeadOfOffice,
            HeadOfDivision,
            DeputyDirector,
            Director,
            HrAdmin,
            HrApprover,
            HrReportAdmin,
            GeneralReportAdmin,
            Supervisor,
            Smd,
            SmdApprover,
            SmdOutcomeEvaluator,
            SecurityAdmin
        ];
    }
}
