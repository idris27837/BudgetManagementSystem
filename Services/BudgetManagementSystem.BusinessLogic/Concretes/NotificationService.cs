using BudgetManagementSystem.BusinessLogic.Abstractions;
using BudgetManagementSystem.DataAccessLayer.Context;
using BudgetManagementSystem.Models.Core;
using BudgetManagementSystem.Models.ErpModel;
using BudgetManagementSystem.Infrastructure.Concrete;
using Microsoft.Extensions.Logging;
using System.Text;
#nullable disable
namespace BudgetManagementSystem.BusinessLogic.Concrete
{
    public class NotificationService
    {
        private EmailServiceDBContext _emailServiceDBContext { get; }
        private ILogger<NotificationService> _logger { get; }
        private IGlobalSetting _globalSetting { get; }

        public NotificationService(EmailServiceDBContext emailServiceDBContext, ILogger<NotificationService> logger, IGlobalSetting globalSetting)
        {
            _emailServiceDBContext = emailServiceDBContext;
            _logger = logger;
            _globalSetting = globalSetting;
        }

        public async Task SaveEmailToDb(string emailTo, string subject, string body, string action)
        {
            try
            {
                var SENDER_EMAIL = "pms@cbn.gov.ng";
                var ENABLE_EMAIL_NOTIFICATION = false;
                try
                {
                    SENDER_EMAIL = await _globalSetting.GetStringValue("SENDER_EMAIL");
                    ENABLE_EMAIL_NOTIFICATION = await _globalSetting.GetBooleanValue("ENABLE_EMAIL_NOTIFICATION");
                }
                catch { }
                if (ENABLE_EMAIL_NOTIFICATION)
                {
                    var mail_request = new EmailObjects()
                    {
                        From = SENDER_EMAIL,
                        To = emailTo,
                        Subject = subject,
                        Body = body,
                        Action = action,
                        Status = "New"
                    };

                    await _emailServiceDBContext.EmailLogs.AddAsync(mail_request);
                    await _emailServiceDBContext.SaveChangesAsync();

                    _logger.LogInformation($"Email saved successfully");
                }
                else
                {
                    _logger.LogWarning($"Email notification is not enabled");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Email Saving Exception: {ex.Message}");
            }
        }
    }
}
