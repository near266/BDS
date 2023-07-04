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
    public class RepostSalePostWorker : BackgroundService
    {
        private readonly IWorkerRepositories _repository;
        private readonly ApplicationDatabaseContext _context;
        private readonly ILogger<RepostSalePostWorker> _logger;

        public RepostSalePostWorker( IServiceProvider serviceProvider,  ILogger<RepostSalePostWorker> logger)
        {
            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDatabaseContext>();
            _repository = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IWorkerRepositories>();
            _logger = logger;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await RepostSale(stoppingToken);
            }

        }
        private async Task RepostSale(CancellationToken cancellationToken)
        {
            try
            {
                var TimeRepost = DateTime.Now;
                var checkRepost = await _context.SalePosts.Where(i => i.IsRepost == true && i.DueDate <= TimeRepost && i.Status == 5).ToListAsync();
                if (checkRepost.Any())
                {

                    foreach (var item in checkRepost)
                    {
                        try
                        {
                            await _repository.RepostSalePost(item.Id, item.Type, cancellationToken);
                        }
                        catch
                        {
                            _logger.LogError($"Sale Post id{item.Id} fail ");
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Repost Sale Fail {ex.Message}");
            }
        }
    }
}
