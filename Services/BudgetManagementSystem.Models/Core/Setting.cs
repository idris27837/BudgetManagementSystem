using Audit.EntityFramework;
using BudgetManagementSystem.Models.BudgetMgt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
#nullable disable
namespace BudgetManagementSystem.Models.Core
{
    [AuditInclude]
    [Table("Settings", Schema = "pms")]
    [PrimaryKey("SettingId")]
    public class Setting : BaseEntity {
        public string SettingId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool IsEncrypted { get; set; } = false;
    }

    public static class SettingType
    {
        public const string Bool = "Bool";
        public const string DateTime = "DateTime";
        public const string Decimal = "Decimal";
        public const string Double = "Double";
        public const string Float = "Float";
        public const string Int = "Int";
        public const string Long = "Long";
        public const string String = "String";


        public static List<string> GetSettingTypeList()
        {
            return new List<string>
        {
            Bool, DateTime, Decimal, Double, Float, Int, Long, String
        };
        }
        public static Dictionary<string, string> GetSettingTypes()
        {
            return new Dictionary<string, string>
        {
            {Bool,"Bool"},
            {DateTime,"DateTime"},
            {Decimal,"Decimal"},
            {Double,"Double"},
            {Float,"Float"},
            {Int,"Int"},
            {Long,"Long"},
            {String,"String"},

        };
        }
    }
}
