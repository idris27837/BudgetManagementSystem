global using BudgetManagementSystem.DataAccessLayer.Context;
global using BudgetManagementSystem.Infrastructure.Abstractions;
global using BudgetManagementSystem.Models;
global using BudgetManagementSystem.Models.AbstractModel;
global using BudgetManagementSystem.Models.Constants;
global using BudgetManagementSystem.Models.Core;
global using BudgetManagementSystem.Models.Organogram;
global using Hangfire;
global using Hangfire.PostgreSql;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using System.Data;
global using System.Linq.Expressions;
global using System.Reflection;
using BudgetManagementSystem.Infrastructure.Concrete;
using BudgetManagementSystem.DataAccessLayer.Context;

namespace BudgetManagementSystem.DataAccessLayer;

public static class DALStartupDependencies
{
    public static IServiceCollection AddDALApplicationDependencies(this IServiceCollection services, IConfiguration Configuration)
    {

#pragma warning disable CS0618 // Type or member is obsolete
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(Configuration.GetConnectionString("HangfireConnection"),
            new PostgreSqlStorageOptions
            {
                SchemaName = Configuration["HangfireSchema:SchemaName"],
            }));
#pragma warning restore CS0618 // Type or member is obsolete

        // Add the processing server as IHostedService
        services.AddHangfireServer();

        services.AddDbContext<CompetencyCoreDbContext>(options =>
                   options.UseNpgsql(Configuration.GetConnectionString(DALConstants.CoreConnectionName)));

        services.AddDbContext<ErpDataDbContext>(options =>
                  options.UseSqlServer(Configuration.GetConnectionString(DALConstants.ERPConnectionName)));
        services.AddDbContext<StaffIDMaskDBContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString(DALConstants.StaffIDMaskConnectionName)));

        services.AddDbContext<EmailServiceDBContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString(DALConstants.EmailServiceConnectionName)));
        
        services.AddDbContext<SasDBContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString(DALConstants.SasConnectionName)));
        
      
        services.AddScoped<ErpEmployeeService>();
        services.AddScoped<SeedERPOrganizationData>();

        return services;
    }
}

public sealed class DALConstants
{
    public const string CoreConnectionName = "CoreDbConnection";
    public const string ERPConnectionName = "ErpDataConnection";
    public const string StaffIDMaskConnectionName = "StaffIDMaskConnection";
    public const string EmailServiceConnectionName = "EmailServiceConnection";
    public const string SasConnectionName = "SasConnection";
}
