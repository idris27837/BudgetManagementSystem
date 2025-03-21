using System.Text.Json.Serialization;

namespace BudgetManagementSystem.Infrastructure.CustomConfig.BitlyVm;

public class BitlyResponseModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("link")]
    public string Link { get; set; }
}

