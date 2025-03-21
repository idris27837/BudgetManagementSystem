using BudgetManagementSystem.ViewModels.UserRoleMgtVm;
using Microsoft.AspNetCore.Components.Forms;

namespace BudgetManagementSystem.BusinessLogic.Abstractions;

public interface IRestWebClient
{
    Task<T> GetUrlAndDeSerialze<T>(string url);

    Task<T> PostUrlAndDeSerialize<T, R>(string url, R r) where T : BaseAPIResponse;
    Task<T> PostFileUrlAndDeSerialize<T>(string url, IBrowserFile file) where T : BaseAPIResponse;
    Task<T> DeleteUrlAndDeSerialize<T>(string url) where T : BaseAPIResponse;
}
