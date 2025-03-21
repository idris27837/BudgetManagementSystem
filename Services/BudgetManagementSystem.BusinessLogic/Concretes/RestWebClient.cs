using BudgetManagementSystem.BusinessLogic.Abstractions;
using BudgetManagementSystem.ViewModels.UserRoleMgtVm;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BudgetManagementSystem.BusinessLogic.Concretes;
#nullable disable
public class RestWebClient : IRestWebClient
{
    private readonly HttpClient httpClient;

    public RestWebClient(HttpClient _httpClient)
    {
        httpClient = _httpClient;
    }

    readonly JsonSerializerOptions serializeOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<T> GetUrlAndDeSerialze<T>(string url)
    {
        try
        {
           
            var result = await httpClient.GetAsync(url);
            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, serializeOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return default;
        }

    }


    public async Task<T> PostUrlAndDeSerialize<T, R>(string url, R r) where T : BaseAPIResponse
    {
        try
        {

            var json = JsonSerializer.Serialize(r);

            var result = await httpClient.PostAsync(url, new StringContent(json, Encoding.Default, "application/json"));

            // Read the result as string
            var content = await result.Content.ReadAsStringAsync();

            // De-Serialize content to match the object for return
            var jsonResponse = JsonSerializer.Deserialize<T>(content, serializeOptions);
            jsonResponse.IsSuccess = result.IsSuccessStatusCode;
            return jsonResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return default;
        }

    }

    public async Task<T> PostFileUrlAndDeSerialize<T>(string url, IBrowserFile file)  where T : BaseAPIResponse
    {
        try
        {
            using var content = new MultipartFormDataContent();



            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            content.Add(content: fileContent, name: "\"file\"", fileName: file.Name);

            var result = await httpClient.PostAsync(url, content);

            // Read the result as string
            var responseContent = await result.Content.ReadAsStringAsync();

            // De-Serialize content to match the object for return
            var jsonResponse = JsonSerializer.Deserialize<T>(responseContent, serializeOptions);
            jsonResponse.IsSuccess = result.IsSuccessStatusCode;

            return jsonResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return default;
        }

    }

    public async Task<T> DeleteUrlAndDeSerialize<T>(string url) where T : BaseAPIResponse
    {
        try
        {
            var result = await httpClient.DeleteAsync(url);

            // Read the result as string
            var content = await result.Content.ReadAsStringAsync();

            // De-Serialize content to match the object for return
            var jsonResponse = JsonSerializer.Deserialize<T>(content, serializeOptions);
            jsonResponse.IsSuccess = result.IsSuccessStatusCode;
            return jsonResponse;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return default;
        }
    }
}
