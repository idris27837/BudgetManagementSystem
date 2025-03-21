namespace BudgetManagementSystem.Infrastructure.Concrete;

public sealed class EmailHtmlTemplate
{
    private static readonly EmailHtmlTemplate _instance = new EmailHtmlTemplate();
    public static EmailHtmlTemplate Instance
    {
        get { return _instance; }
    }

    public EmailHtmlTemplate()
    {

    }
    public string GetAccountCreationTemplate()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\EmailTemplate\\accountcreation.html");

        using StreamReader reader = new StreamReader(path);
        return reader.ReadToEnd();
    }

    public string GetSignupTemplate()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\EmailTemplate\\signup.html");

        using StreamReader reader = new StreamReader(path);
        return reader.ReadToEnd();
    }
    public string GetResetPasswordTemplate()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\EmailTemplate\\resetpassword.html");

        using StreamReader reader = new StreamReader(path);
        return reader.ReadToEnd();
    }

}

