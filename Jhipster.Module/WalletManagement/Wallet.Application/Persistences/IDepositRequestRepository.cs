using Jhipster.Crosscutting.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;

namespace Wallet.Application.Persistences
{
    public interface IDepositRequestRepository
    {
        Task<int> Add(DepositRequest rq);
        Task<int> Update(Guid id, int status, CancellationToken cancellationToken);
        // Task<int> Delete(Guid rq);
        Task<PagedList<DepositRequest>> GetByAdmin(int Page, int PageSize,string? UserName, DateTime? DateTo,DateTime? DateFrom);
        Task<List<DepositRequest>> GetByUser(Guid Id);
    }
}
