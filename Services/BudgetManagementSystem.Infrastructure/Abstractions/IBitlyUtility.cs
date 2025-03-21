namespace BudgetManagementSystem.Infrastructure.Abstractions;

public interface IBitlyUtility
{
    Task<string> GenerateShortReferralLink(string longUrl);
}
