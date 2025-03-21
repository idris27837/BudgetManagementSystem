namespace BudgetManagementSystem.Models.ErpModel
{
    public class StaffIDMaskDetails
    {
        public int StaffIdMaskId { get; set; }
        public string Name { get; set; }
        public string EmployeeNumber { get; set; }
        public byte[] CurrentPicture { get; set; }
        public byte[] NewPicture { get; set; }
        public string BloodGroup { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string RejectReason { get; set; }
        public string Rejectedby { get; set; }
        public DateTime RejectionDate { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool MessageStatus { get; set; }
    }
}
