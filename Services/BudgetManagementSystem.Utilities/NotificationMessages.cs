

namespace System
{
    public static class NotificationMessages
    {
       
        public const string NoRecordFound = "No Record Found";
        public const string OperationCompleted = "Operation Completed successfully";
        public const string InvalidDateFormat = "Invalid Date Format Expected Format (yyyyMMdd)";
        public const string TryAgainLaterError =
            "Oops! Something went wrong! Please try again later. If this error continues to occur, please contact our support center";

        public const string InvalidCredentials = "Invalid email or password";
        public const string PasswordChangeSuccessful = "Password changed successfully";

        public const string InvalidPassword = "Invalid password";

        public const string LoginLockedOut =
            "Your account is locked because of too many invalid login attempts. Please try again in a few minutes.";

        public const string TwoFactorAuthenticationCodeInvalid = "Invalid code";
        public const string TwoFactorAuthenticationEnabled = "Two-factor authentication enabled successfully";
        public const string TwoFactorAuthenticationDisabled = "Two-factor authentication disabled successfully";

        public const string TwoFactorAuthenticationDisableError =
            "An error occured while disabling two-factor authentication";

        public const string SessionExpired = "Session has expired. Please log in again.";

        public const string EmailVerificationLinkResentSuccessfully =
            "Account activation link sent successfully";

        public const string SuccessfulEmailVerification =
            "Account activated. You can now log in.";

        public const string EmailVerificationFailed =
            "An error occured while verifying your email. Please try again later and if this error continues to occur, contact our support center";

        public const string EmailAlreadyVerified = "Your email is already verified. You can log in.";

        public const string EmailVerificationRequired =
            "Your account is not activated. If you have not received the activation email, you can request a new one on this page.";
        
        public const string GenericException ="Process could not be completed.";
    }
    public static class NotificationTemplates
    {

        public const string SlaGenericNewRequest = "<p>Dear %NAME%</p>" +
                                      "<p>A request on %REQUEST_NAME% has been assigned to you on %ASSIGNED_DATE%.</p>" +
                                      "<p>Kindly logon to your account on Performance Management System to treat this request as soon as possible to avoid breaching SLA of %SLA_HOURS% hours after initiation.</p>" +
                                      "<p>Thank you, </br>CBN PMS></p>";
        
        public const string GenericNewRequest = "<p>Dear %NAME%</p>" +
                                      "<p>A request on %REQUEST_NAME% has been assigned to you on %ASSIGNED_DATE%</p>" +
                                      "<p>Kindly logon to your account on Performance Management System to treat this request.</p>" +
                                      "<p>Thank you, </br>CBN PMS></p>";
        
        public const string AssignerGenericNewRequest = "<p>Dear %NAME%</p>" +
                                      "<p>Your request on %REQUEST_NAME% has been initated on %ASSIGNED_DATE%</p>" +
                                      "<p>Thank you, </br>CBN PMS></p>";
        
        public const string UpdateRequest = "<p>Dear %NAME%</p>" +
                                      "<p>You have treated the  request on %REQUEST_NAME% on %TREATED_DATE%</p>" +
                                      "<p>Thank you, </br>CBN PMS></p>";
        
        public const string AssignerUpdateRequest = "<p>Dear %NAME%</p>" +
                                      "<p>Your request on %REQUEST_NAME% has been treated on %TREATED_DATE%</p>" +
                                      "<p>Thank you, </br>CBN PMS></p>";
        
    }
}