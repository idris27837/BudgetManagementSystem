using PMS.Models.PerformanceMgt.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManagementSystem.Models.CompetencyMgt;
using BudgetManagementSystem.ViewModels.UserRoleMgtVm;
using Microsoft.AspNetCore.Http;
using BudgetManagementSystem.Models.PerformanceMgt;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManagementSystem.ViewModels.PMSVms.SetupVm
{
    public class StrategyVm : BaseWorkFlow
    {
        public string StrategyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BankYearId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public object ImageFile { get; set; }
    }
    public sealed class StrategyListVm : BaseAPIResponse
    {
        public List<StrategyVm> BankStrategies { get; set; }
        public int TotalRecord { get; set; }
    }

    public sealed class SearchStrategyVm : BasePagedData
    {
        public int? CategoryId { get; set; }

        public string SearchString { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsRejected { get; set; }
        public bool? IsTechnical { get; set; }
    }

    public class CreateNewStrategyVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BankYearId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public object ImageFile { get; set; }
        public bool? IsActive { get; set; }
    }

    #region theme
    public class StrategicThemeVm : BaseWorkFlow
    {
        public string StrategicThemeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StrategyId { get; set; }
        public string StrategyName { get; set; }
        public object ImageFile { get; set; }

    }
    public class CreateStrategicThemeVm : BaseWorkFlow
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string StrategyId { get; set; }
        public object ImageFile { get; set; }
    }

    public sealed class StrategicThemeListVm : BaseAPIResponse
    {
        public List<StrategicThemeVm> StrategicThemes { get; set; }
        public int TotalRecord { get; set; }
    }
    #endregion



}
