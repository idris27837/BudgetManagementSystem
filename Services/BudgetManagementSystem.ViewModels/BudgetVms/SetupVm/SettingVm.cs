using BudgetManagementSystem.Models.BudgetMgt;
using BudgetManagementSystem.ViewModels.UserRoleMgtVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagementSystem.ViewModels.BudgetVms.SetupVm
{
    public class SettingVm : BaseEntity
    {
        public string SettingId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsEncrypted { get; set; } = false;

    }

    #region setting
    public class SettingResponseDetail : BaseEntity
    {
        public string SettingId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsEncrypted { get; set; }
    
    }
    public class SettingResponse : BaseAPIResponse
    {
        public SettingResponseDetail Data { get; set; }
        public string StatusCode { get; set; }
        public string ActionCall { get; set; }
    }

    public class ListSettingResponse : BaseAPIResponse
    {
        public List<SettingResponseDetail> Data { get; set; }
        public int TotalSettings { get; set; } = 0;
        public string StatusCode { get; set; }
        public string ActionCall { get; set; }
    }
    #endregion

    public class CommonExceptionResponse : BaseAPIResponse
    {
        public string StatusCode { get; set; }
        public string ActionCall { get; set; }
    }
}
