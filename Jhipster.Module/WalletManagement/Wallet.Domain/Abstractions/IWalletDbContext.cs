using Microsoft.EntityFrameworkCore;
using System;
using Wallet.Domain.Entities;

namespace Wallet.Domain.Abstractions
{
	public interface IWalletDbContext
	{
        public DbSet<Customer> Customers { get; set; }
        public DbSet<WalletEntity> Wallets { get; set; }
        public DbSet<WalletPromotional> WalletPromotionals { get; set; }
        public DbSet<TransactionHistory> TransactionHistorys { get; set; }
        public DbSet<DepositRequest> DepositRequests { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

