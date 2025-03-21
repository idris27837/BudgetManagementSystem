namespace BudgetManagementSystem.Models.ErpModel
{
    public class EmployeeDetails
    {
        public int OrganizationId { get; set; }
        public int PersonId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public int PersonTypeId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleNames { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public string AssignmentNumber { get; set; }
        public DateTime OriginalDateOfHire { get; set; }
        public int OfficeId { get; set; }
        public string SupervisorId { get; set; }
        public string JobName { get; set; }
        public string HeadOfOfficeId { get; set; }
        public int LocationId { get; set; }
        public string LocationCode { get; set; }
        public string MobileNumber { get; set; }
        public string Status { get; set; }
        public DateTime? TerminationDate { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string HeadOfDeptId { get; set; }
        public string HeadOfDeptName { get; set; }
        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string HeadOfDivId { get; set; }
        public string HeadOfDivName { get; set; }
        public string OfficeName { get; set; }
        public int? GradeId { get; set; }
        public string Grade { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string BankAccountNumber { get; set; }
        public string Religion { get; set; }
        public string StateOfOrigin { get; set; }
        public string SenatorialDistrict { get; set; }
        public string Lga { get; set; }
        public string BankCode { get; set; }
        public string GeoPoliticalZone { get; set; }
        public string FullName { get; set; }
        public int? AssignmentId { get; set; }
        public string Position { get; set; }
        public string WalletId { get; set; }
        public string WalletAlias { get; set; }
        public string NameInitial => !string.IsNullOrEmpty(LastName) ? $"{LastName[0]}{FirstName[0]}" : "";

    }
}
