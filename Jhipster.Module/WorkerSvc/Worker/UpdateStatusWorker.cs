using Jhipster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerSvc.Application.Persistences;

namespace Worker
{
    public class UpdateStatusWorker : BackgroundService
    {
        private readonly IWorkerRepositories _repository;
        private readonly ILogger<UpdateStatusWorker> _logger;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDatabaseContext _context;
        public UpdateStatusWorker(IConfiguration configuration,
            IServiceProvider serviceProvider, ILogger<UpdateStatusWorker> logger)
        {
            _repository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IWorkerRepositories>();
            _logger = logger;
            _configuration = configuration;
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDatabaseContext>();
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckStatus();
                await Task.Delay(_configuration.GetValue<int>("TimeSend"), stoppingToken);
            }
        }
        private async Task CheckStatus()
        {
            try
            {
                var TimeNow = DateTime.Now;
                var check = await _context.SalePosts.Where(i => i.DueDate <= TimeNow && i.Status != 2).ToListAsync();
                if (check.Any())
                {
                    _logger.LogInformation($"[WORKER] Update Sale Post running at: {DateTimeOffset.Now}");
                    foreach (var item in check)
                    {
                        try
                        {
                            await _repository.UpdateStatus(item.Id, 5);

                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning($"Update SalePort Fail Id{item.Id}. Please send again. Code: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Update Status SalePost Fail- {exception}", ex.Message);
            }
        }
    }
}
