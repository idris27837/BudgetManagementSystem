namespace BudgetManagementSystem.Infrastructure.Concrete;

public static class ValidateExcelData
{
    public static bool CheckDataForTrueFalse(List<string> data)
    {
        bool status = false;
        foreach (var item in data)
        {
            status = ValidateYesNo(item);
        }
        return status;
    }

    public static bool ValidateYesNo(string item)
    {
        bool status = false;
        if (item.ToUpper().Equals("TRUE") || item.ToUpper().Equals("YES"))
        {
            status = true;
        }
        return status;
    }
}
public static class CurrencyHelper
{

}


