using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagementSystem.BusinessLogic.Abstractions {
    public interface IGlobalSetting
    {
        Task<bool> GetBooleanValue(string Key);
        Task<DateTime> GetDateTimeValue(string Key);
        Task<Decimal> GetDecimalValue(string Key);
        Task<Double> GetDoubleValue(string Key);
        Task<float> GetFloatValue(string Key);
        Task<int> GetIntValue(string Key);
        Task<long> GetLongValue(string Key);
        Task<string> GetStringValue(string Key);
    }
}
