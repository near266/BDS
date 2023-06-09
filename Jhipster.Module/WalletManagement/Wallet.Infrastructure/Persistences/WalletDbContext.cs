using System;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistences
{
	public class WalletDbContext : DbContext, IWalletDbContext
    {
        public DbSet<WalletEntity> Wallets { get; set; }
        public DbSet<WalletPromotional> WalletPromotionals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TransactionHistory> TransactionHistorys { get; set; }
        public DbSet<DepositRequest> DepositRequests { get; set; }

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

