using System;
namespace Wallet.Domain.Abstractions
{
	public interface IWalletDbContext
	{
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

