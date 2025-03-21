using BudgetManagementSystem.BusinessLogic.Abstractions;
using BudgetManagementSystem.BusinessLogic.Concrete;

namespace BudgetManagementSystem.BusinessLogic;

public static class BusinessLogicDependenciesExtension
{
    public static IServiceCollection AddBusinessLogicDependenciesExtension(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
    {
        services.AddScoped<IReviewPeriodService, ReviewPeriodService>();
        services.AddScoped<IPmsSetupService, PmsSetupService>();
        services.AddScoped<IGrievanceManagementService, GrievanceManagementService>();
        services.AddScoped<IPerformanceManagementService, PerformanceManagementService>();
        services.AddScoped<IPmsGlobalConfiguration, PmsGlobalConfiguration>();
        services.AddScoped<IGlobalSetting, GlobalSetting>();
        services.AddScoped(typeof(IManagedAESEncryption), typeof(ManagedAESEncryption));
        services.AddScoped<AppKey>();
        services.AddScoped<NotificationService>();
        services.AddHostedService<ReviewPeriodBackgroundService>();
        services.AddHostedService<CompetencyClosureBackgroundService>();
        services.AddHostedService<AutoReassignRequestBackgroundService>();

        return services;
    }

}
