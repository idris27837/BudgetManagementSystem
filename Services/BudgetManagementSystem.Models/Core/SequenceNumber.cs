

using BudgetManagementSystem.Models;
using BudgetManagementSystem.Models.BudgetMgt;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManagementSystem.Models.Core
{
    [Table("SequenceNumber", Schema = "pms")]
    [PrimaryKey("Description")]
    public partial class SequenceNumber : BaseEntity
    {
        public SequenceNumberTypes SequenceNumberType { get; set; }
        public string Description { get; set; }
        public string Prefix { get; set; }
        public long NextNumber { get; set; }
        public bool UsePrefix { get; set; }
    }
}
