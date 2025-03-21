namespace BudgetManagementSystem.Infrastructure.Abstractions;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message, string fullName = "");
    Task SendEmailAsync(string[] emails, string subject, string message);
}
