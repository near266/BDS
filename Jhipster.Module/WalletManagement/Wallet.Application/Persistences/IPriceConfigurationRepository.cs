using Jhipster.Crosscutting.Utilities;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.Persistences
{
    public interface IPriceConfigurationRepository
    {
        Task<int> Add(PriceConfiguration priceConfiguration, CancellationToken cancellationToken);
        Task<int> Update(PriceConfiguration priceConfiguration, CancellationToken cancellationToken);
        Task<int> Delete(List<Guid> Id, CancellationToken cancellationToken);
        Task<List<PriceConfiguration>> GetPriceConfigurationByTypePriceId(Guid TypePriceId);
    }
}
