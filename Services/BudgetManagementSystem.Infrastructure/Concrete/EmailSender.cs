using BudgetManagementSystem.Infrastructure.Abstractions;

namespace BudgetManagementSystem.Infrastructure.Concrete;

public sealed class EmailSender : IEmailSender
{
    private readonly string SchoolName;
    private readonly string Logo;
    private readonly string NoReplyEmail;
    private readonly string APIKey;
    public EmailSender(IOptions<GeneralConfiguration> generalConfig, IOptions<SenGridApiKey> senGridApi)
    {
        SchoolName = generalConfig.Value.Name;
        Logo = generalConfig.Value.Logo;
        NoReplyEmail = generalConfig.Value.NoReplyEmail;
        APIKey = senGridApi.Value.ApiKey;
    }

    public async Task SendEmailAsync(string email, string subject, string message, string fullName = "")
    {
        var client = new SendGridClient(APIKey);

        var from = new EmailAddress(NoReplyEmail, SchoolName);
        var to = new EmailAddress(email, fullName);

        message = message.Replace("[[LOGO]]", Logo);
        message = message.Replace("[[SCHOOLNAME]]", SchoolName);


        var plainTextContent = message;
        var htmlContent = message;
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        try
        {
            var response = await client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task SendEmailAsync(string[] emails, string subject, string message)
    {

        var client = new SendGridClient(APIKey);
        var from = new EmailAddress(NoReplyEmail, SchoolName);

        message = message.Replace("[[LOGO]]", Logo);
        message = message.Replace("[[SCHOOLNAME]]", SchoolName);

        var to = new List<EmailAddress>(emails.Select(s => new EmailAddress { Email = s }));
        var plainTextContent = message;
        var htmlContent = message;
        var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, to, subject, plainTextContent, htmlContent);
        try
        {
            var response = await client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

