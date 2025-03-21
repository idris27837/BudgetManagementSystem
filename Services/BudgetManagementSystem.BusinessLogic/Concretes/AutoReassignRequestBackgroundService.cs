using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BudgetManagementSystem.BusinessLogic.Abstractions;
using BudgetManagementSystem.DataAccessLayer.Context;
using BudgetManagementSystem.ViewModels.PMSVms;
using ExcelDataReader.Log;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PMS.Models;
using PMS.Models.PerformanceMgt.CoreModels;

namespace BudgetManagementSystem.BusinessLogic.Concretes
{
    public class AutoReassignRequestBackgroundService : BackgroundService
    {
        private readonly ILogger<AutoReassignRequestBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public AutoReassignRequestBackgroundService(ILogger<AutoReassignRequestBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<CompetencyCoreDbContext>();
            var _globalSetting = scope.ServiceProvider.GetRequiredService<IGlobalSetting>();
            var _pmsGlobalSetting = scope.ServiceProvider.GetRequiredService<IPmsGlobalConfiguration>();
            var _performanceManagementService = scope.ServiceProvider.GetRequiredService<IPerformanceManagementService>();
            var _erpEmployee = scope.ServiceProvider.GetRequiredService<ErpEmployeeService>();
             
            while (true)  // Outer loop to restart the service
            {
                try
                {
                    _logger.LogInformation("Auto re-assign background service starting...");
                    await RunServiceAsync(stoppingToken, _context, _globalSetting, _pmsGlobalSetting, _performanceManagementService);
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogCritical($"Auto re-assign background service was stopped. Restarting... Error: {ex.Message}");

                    // Optional delay before restarting the service
                    await Task.Delay(5000);

                    // Continue the outer loop to restart the service
                    continue;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Unhandled exception occurred: {ex.Message}");
                    break; // Break the loop if a critical exception occurs
                }
            }
        }
        private async Task RunServiceAsync(CancellationToken stoppingToken, CompetencyCoreDbContext _context, IGlobalSetting _globalSetting, IPmsGlobalConfiguration _pmsGlobalSetting, IPerformanceManagementService  _performanceManagementService)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // get service enable status
                    var ENABLE_AUTO_REASSIGN_REQUEST_BACKGROUND_SERVICE = false;
                    try
                    {
                        ENABLE_AUTO_REASSIGN_REQUEST_BACKGROUND_SERVICE = await _globalSetting.GetBooleanValue("ENABLE_AUTO_REASSIGN_REQUEST_BACKGROUND_SERVICE");
                    }
                    catch { }
                    // run if task is enabled
                    if (ENABLE_AUTO_REASSIGN_REQUEST_BACKGROUND_SERVICE)
                    {
                        _logger.LogInformation("Auto re-assign background service executing...");

                        try
                        {
                            var today = DateTime.Now.Date;

                            // get active requests
                            var requests_response = await _performanceManagementService.GetPendingRequests();
                            if (requests_response.IsSuccess)
                            {
                                var requests = requests_response.Requests;
                                var breachedRequest = requests.Where(x => x.IsBreached);
                                foreach (var request in breachedRequest)
                                {
                                    //queue Competency Closure
                                    BackgroundJob.Enqueue(() => _performanceManagementService.AutoReassignAndLogRequestAsync(request.FeedbackRequestLogId));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogCritical(ex.Message);
                        }
                    }

                    // add a delay to not run in a tight loop 600000 ms = 600 secs = 10 mins
                    await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                    //Thread.Yield();
                    //}
                }
                catch (OperationCanceledException)
                {
                    // If the token was cancelled, rethrow the exception to trigger restart logic
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occurred during execution: {ex.Message}");
                    // Handle other exceptions if necessary, but allow the loop to continue
                }
            }
        }
    }
}



