using Jhipster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkerSvc.Application.Persistences;

namespace Worker
{
    public class UpdateOrderFakeNewWorker : BackgroundService
    {
        private readonly IWorkerRepositories _repository;
        private readonly ILogger<UpdateOrderFakeNewWorker> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDatabaseContext _context; 
        private const string Schedule = "59 59 23 * * *";
        private readonly CrontabSchedule _crontabSchedule;
        private DateTime _nextRun;

        public UpdateOrderFakeNewWorker(IConfiguration configuration,
            IServiceProvider serviceProvider, ILogger<UpdateOrderFakeNewWorker> logger)
        {
            _repository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IWorkerRepositories>();
            _logger = logger;
            _crontabSchedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _configuration = configuration;
            _nextRun = _crontabSchedule.GetNextOccurrence(DateTime.Now);
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDatabaseContext>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Run(async () =>
                    {
                        while (!stoppingToken.IsCancellationRequested)
                        {
                            await Task.Delay(UntilNextExecution(), stoppingToken);

                            /// do something
                            _nextRun = _crontabSchedule.GetNextOccurrence(DateTime.Now);
                        }
                    }, stoppingToken);

                }
                catch (Exception ex)
                {
                    _logger.LogError("Stop handler: Immediately - {exception}", ex.Message);
                }


            }
        }
        private int UntilNextExecution() => Math.Max(0, (int)_nextRun.Subtract(DateTime.Now).TotalMilliseconds);

    
    private async Task UpdateFake()
        {
            try
            {
                var TimeNow = DateTime.Now;
                var check = await _context.FakeNew.ToListAsync();
                if (check.Any())
                {
                    _logger.LogInformation("[WORKER] Update fake Post running at: {time}", DateTimeOffset.Now);
                    foreach (var item in check)
                    {
                        try
                        {
                            await _repository.UpdateOrderFakeNew(item.Id);

                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning($"Update fake new Fail Id{item.Id}. Please send again. Code: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Update fake new Fail- {exception}", ex.Message);
            }
        }
    }
}
