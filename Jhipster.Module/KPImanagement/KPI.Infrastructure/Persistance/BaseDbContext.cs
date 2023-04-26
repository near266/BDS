using System;
using Jhipster.Infrastructure.Shared;
using KPI.Core.Abtractions;
using Microsoft.EntityFrameworkCore;

namespace KPI.Infrastructure.Persistance
{
    public class BaseDbContext : ModuleDbContext, IBaseDbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(true, cancellationToken);
        }

    }
}

