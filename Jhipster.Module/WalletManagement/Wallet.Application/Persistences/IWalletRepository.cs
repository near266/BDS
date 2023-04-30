using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.Persistences
{
    public interface IWalletRepository
    {
        Task<int> Add(WalletEntity Wallet,CancellationToken cancellation);
        Task<int> Update(WalletEntity Wallet,CancellationToken cancellation);
        Task<int> Delete(Guid Id,CancellationToken cancellation);
        Task<IEnumerable<WalletEntity>> GetAll();
    }
}
