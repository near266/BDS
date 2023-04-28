using Microsoft.EntityFrameworkCore;
using System;
using Wallet.Domain.Entities;

namespace Wallet.Domain.Abstractions
{
	public interface IWalletDbContext
	{
        public DbSet<WalletEntity> Wallets { get; set; }
        public DbSet<WalletPromotional> WalletsPromotional { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

