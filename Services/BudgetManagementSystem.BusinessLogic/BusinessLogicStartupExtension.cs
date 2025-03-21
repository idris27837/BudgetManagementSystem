global using BudgetManagementSystem.BusinessLogic.Commands;
global using BudgetManagementSystem.BusinessLogic.Concretes;
global using BudgetManagementSystem.BusinessLogic.Queries;
global using BudgetManagementSystem.DataAccessLayer.Concrete;
global using BudgetManagementSystem.Infrastructure.Abstractions;
global using BudgetManagementSystem.Infrastructure.Concrete;
//global using BudgetManagementSystem.Models.CompetencyMgt;
//global using BudgetManagementSystem.Models.Organogram;
global using BudgetManagementSystem.ViewModels;
//global using BudgetManagementSystem.ViewModels.OrganogramVm;
global using Mediator;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;

namespace BudgetManagementSystem.BusinessLogic;

public static class BusinessLogicStartupExtension
{
    public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection services)
    {
        services.AddScoped(typeof(IDateTimeService), typeof(DateTimeService));
        services.AddScoped(typeof(IPasswordGenerator), typeof(PasswordGenerator));
        services.AddScoped<ReviewAgentService>();
        services.AddScoped<ActiveDirectoryService>();


        //services.AddScoped(typeof(IDapperRepo<>), typeof(DapperRepo<>));
        services.AddScoped(typeof(IRepo<>), typeof(BaseRepo<>));
        services.AddScoped(typeof(IPMSRepo<>), typeof(PerformanceRepo<>));

        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);


        return services;
    }

}
