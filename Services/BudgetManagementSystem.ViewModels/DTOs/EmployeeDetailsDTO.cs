namespace BudgetManagementSystem.ViewModels.DTOs;

public class EmployeeErpDetailsDTO
{
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string FirstName { get; set; }
    public string MiddleNames { get; set; }
    public string LastName { get; set; }
    public string EmployeeNumber { get; set; }
    public string JobName { get; set; }
    public string DepartmentName { get; set; }
    public string DivisionName { get; set; }
    public string HeadOfDivName { get; set; }
    public string OfficeName { get; set; }
    public string SupervisorId { get; set; }
    public string HeadOfOfficeId { get; set; }
    public string HeadOfDivId { get; set; }
    public string HeadOfDeptId { get; set; }
    public int? DepartmentId { get; set; }
    public int OfficeId { get; set; }
    public string Grade { get; set; }
    public int? DivisionId { get; set; }
    public string Position { get; set; }

    public string FullName => $"{LastName}, {FirstName} {MiddleNames}";
    public string NameInitial => (!string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(FirstName)) ? $"{LastName[0]}{FirstName[0]}" : "";
    public int PersonId { get; set; }

    #region EXTRAS

    //public int OrganizationId { get; set; }
    //public int PersonId { get; set; }
    //public int PersonTypeId { get; set; }
    //public string LocationCode { get; set; }
    //public string MobileNumber { get; set; }
    //public string HeadOfDeptName { get; set; }
    //public string AssignmentNumber { get; set; }
    //public DateTime OriginalDateOfHire { get; set; }
    //public int LocationId { get; set; }
    //public string Status { get; set; }
    //public DateTime? TerminationDate { get; set; }
    //public int? GradeId { get; set; }
    //public DateTime CreationDate { get; set; }
    //public DateTime? LastUpdateDate { get; set; }
    //public string FullName { get; set; }
    //public int? AssignmentId { get; set; }

    #endregion
}

public class EmployeeData : EmployeeErpDetailsDTO
{
    public string Status { get; set; }
    public int PersonTypeId { get; set; }
    public int LocationId { get; set; }
    public string LocationCode { get; set; }
}
public sealed class ErpOrganizationVm
{
    public int? DepartmentId { get; set; }
    public string DepartmentName { get; set; }

    public int? DivisionId { get; set; }
    public string DivisionName { get; set; }

    public int OfficeId { get; set; }

    public string OfficeName { get; set; }

}

public sealed class ERPOfficeJobRoleVm
{
    public string DivisionCode { get; set; }
    public string JobRoleName { get; set; }
    public string OfficeFullName { get; set; }
    public int OfficeId { get; set; }

    public string OfficeName { get; set; }

}

public sealed class EROJobGradeVm
{
    public string GradeId { get; set; }
    public string GradeName { get; set; }

}
