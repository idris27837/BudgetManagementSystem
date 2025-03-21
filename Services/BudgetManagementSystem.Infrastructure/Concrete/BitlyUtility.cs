using BudgetManagementSystem.Infrastructure.Abstractions;
using BudgetManagementSystem.Infrastructure.CustomConfig.BitlyVm;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BudgetManagementSystem.Infrastructure.Concrete;

public class BitlyUtility : IBitlyUtility
{
    public HttpClient _client;
    private readonly IOptions<BitlyConfig> _bitlyConfig;

    public HttpClient Client
    {
        get
        {
            if (_client == null)
            {
                _client = new HttpClient();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bitlyConfig.Value.Token}");
            }
            return _client;

        }
    }
    public BitlyUtility(IOptions<BitlyConfig> bitlyConfig)
    {
        _bitlyConfig = bitlyConfig;
    }
    public async Task<string> GenerateShortReferralLink(string longUrl)
    {
        var requestBody = new BitlyRequestBody
        {
            GroupGuid = _bitlyConfig.Value.GroupGuid,
            Domain = _bitlyConfig.Value.Domain,
            LongUrl = longUrl
        };
        var request = new HttpRequestMessage(HttpMethod.Post, _bitlyConfig.Value.Url)
        {
            Content = new StringContent(
                                JsonSerializer.Serialize(requestBody),
                                Encoding.UTF8,
                                "application/json"
                            )
        };
        _client = null;
        var response = await Client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<BitlyResponseModel>(content);
            return result.Link;

        }
        return null;
    }
}

