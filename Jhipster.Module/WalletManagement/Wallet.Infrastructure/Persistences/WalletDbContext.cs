using System;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Abstractions;

namespace Wallet.Infrastructure.Persistences
{
	public class WalletDbContext : DbContext, IWalletDbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
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

