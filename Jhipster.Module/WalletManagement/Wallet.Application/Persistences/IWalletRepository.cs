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
        Task<int> Add(WalletEntity Wallet);
        Task<int> Update(WalletEntity Wallet);
        Task<int> Delete(Guid Id);
        Task<IEnumerable<WalletEntity>> GetAll();
    }
}
