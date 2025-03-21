namespace BudgetManagementSystem.Models.Core;

public class EmailObjects
{
    public decimal Id { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string CC { get; set; }
    public string BCC { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string Status { get; set; }
    public int NoOfRetry { get; set; }
    public DateTime? ExpectedSendDate { get; set; }
    public DateTime? ActualSendDate { get; set; }
    public string Action { get; set; }
    public string AppSource { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? DateCreated { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedDate { get; set; }
    public string EmailGuid { get; set; }
}