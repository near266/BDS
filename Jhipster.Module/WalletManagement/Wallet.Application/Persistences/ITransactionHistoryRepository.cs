using Jhipster.Crosscutting.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Domain.Entities;

namespace Wallet.Application.Persistences
{
    public interface ITransactionHistoryRepository
    {
        Task<int> Add(TransactionHistory rq, CancellationToken cancellationToken);
        Task<PagedList<SearchTransactionResponse>> Search(string? Code,string? userid, int? type, DateTime? from, DateTime? to, int Page, int PageSize);
    }
}
