using Microsoft.EntityFrameworkCore;
using System;
using Wallet.Domain.Entities;

namespace Wallet.Domain.Abstractions
{
	public interface IWalletDbContext
	{


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

