using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.Persistences
{
    public interface IWalletPromotionalRepository
    {
        Task<int> Add(WalletPromotional request,CancellationToken cancellationToken);
        Task<int> Update(WalletPromotional request,CancellationToken cancellationToken);
        Task<int> Delete(Guid Id,CancellationToken cancellationToken);
        Task<IEnumerable<WalletPromotional>> GetAll();
    }
}
