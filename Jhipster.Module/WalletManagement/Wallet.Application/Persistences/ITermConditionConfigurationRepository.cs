using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.Persistences
{
    public interface ITermConditionConfigurationRepository
    {
        Task<int> Add(TermConditionConfiguration rq, CancellationToken cancellationToken);
        Task<int> Update(TermConditionConfiguration rq, CancellationToken cancellationToken);
        Task<int> Delete(List<Guid> Id, CancellationToken cancellationToken);
        Task<IEnumerable<TermConditionConfiguration>> GetAll();
        Task<TermConditionConfiguration> ViewDetail(Guid Id);
    }
}
