
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagementSystem.ViewModels.BudgetVms
{
    internal class RequestVms
    {
    }

    #region global settings
    public class AddSettingRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public bool IsEncrypted { get; set; } = false;
    }
    public class SettingRequestModel
    {
        [Required]
        public string SettingId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }

        [Required]
        public string Type { get; set; }
        [Required]
        public bool IsEncrypted { get; set; } = false;
    }
    #endregion

    #region global settings
    public class AddPmsConfigurationRequestModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public bool IsEncrypted { get; set; } = false;
    }
    public class PmsConfigurationRequestModel
    {
        [Required]
        public string PmsConfigurationId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }

        [Required]
        public string Type { get; set; }
        [Required]
        public bool IsEncrypted { get; set; } = false;
    }
    #endregion

    

    #region request log
    public class FeedbackRequestModel
    {
        public string RequestId { get; set; }
        public string AssigneeId { get; set; }
    }
    public class TreatFeedbackRequestModel
    {
        [Required]
        public string RequestId { get; set; }
        [Required]
        public OperationTypes OperationType { get; set; }
        [Required]
        public string Comment { get; set; }
    }
    #endregion
    

    

    

    

}
