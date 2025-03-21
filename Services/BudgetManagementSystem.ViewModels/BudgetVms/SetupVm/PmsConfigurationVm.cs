using BudgetManagementSystem.Models.BudgetMgt;
using BudgetManagementSystem.ViewModels.UserRoleMgtVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManagementSystem.ViewModels.BudgetMgt;

namespace BudgetManagementSystem.ViewModels.BudgetVms.SetupVm
{

    #region Pms Configuration
    public class PmsConfigurationVm : BaseEntity
    {
        public string PmsConfigurationId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsEncrypted { get; set; }
        public bool IsActive { get; set; }
    }

    public class PmsConfigurationResponseVm : BaseAPIResponse
    {
        public PmsConfigurationVm Data { get; set; }
        public string StatusCode { get; set; }
        public string ActionCall { get; set; }
    }

    public class ListPmsConfigurationResponseVm : BaseAPIResponse
    {
        public List<PmsConfigurationVm> Data { get; set; }
        public int TotalSettings { get; set; } = 0;
        public string StatusCode { get; set; }
        public string ActionCall { get; set; }
    }
    #endregion

}
