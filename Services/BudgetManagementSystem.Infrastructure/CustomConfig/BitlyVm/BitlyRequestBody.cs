using System.Text.Json.Serialization;

namespace BudgetManagementSystem.Infrastructure.CustomConfig.BitlyVm;

public sealed class BitlyRequestBody
{
    [JsonPropertyName("group_guid")]
    public string GroupGuid { get; set; }
    [JsonPropertyName("domain")]
    public string Domain { get; set; }
    [JsonPropertyName("long_url")]
    public string LongUrl { get; set; }
}

