using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.Persistences
{
    public interface ITypePriceRepository
    {
        Task<int> Add(TypePrice typePrice, CancellationToken cancellationToken);
        Task<int> Update(TypePrice typePrice, CancellationToken cancellationToken);
        Task<int> Delete(List<Guid> Id, CancellationToken cancellationToken);
        Task <IEnumerable<TypePrice>> GetAll();    
    }
}
