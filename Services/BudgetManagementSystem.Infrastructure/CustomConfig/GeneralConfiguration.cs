namespace BudgetManagementSystem.Infrastructure.CustomConfig;

public sealed class GeneralConfiguration
{
    public string Domain { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public string NoReplyEmail { get; set; }
}

public sealed class SenGridApiKey
{
    public string ApiKey { get; set; }
}

