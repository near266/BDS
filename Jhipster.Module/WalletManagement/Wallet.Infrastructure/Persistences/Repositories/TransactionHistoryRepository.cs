using Jhipster.Crosscutting.Utilities;
using Jhipster.Domain;
using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistences.Repositories
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        private readonly IWalletDbContext _context;

        public TransactionHistoryRepository(IWalletDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(TransactionHistory rq, CancellationToken cancellationToken)
        {
            await _context.TransactionHistorys.AddAsync(rq, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedList<TransactionHistory>> Search(string? userid, int? type, DateTime? from, DateTime? to, int Page, int PageSize)
        {
            var query = _context.TransactionHistorys.AsQueryable();
            if(type != null)
            {
                query = query.Where(x => x.Type == type);
            }
            if(from != null && to != null)
            {
                query = query.Where(i => i.CreatedDate >= from && i.CreatedDate <= to);
            }
            if(userid != null)
            {
                query = query.Where(i => i.CustomerId == Guid.Parse(userid));
            }

            var sQuery = query.Include(i => i.Customer).OrderByDescending(i => i.CreatedDate);
            var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                                .Take(PageSize)
                                .ToListAsync();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<TransactionHistory>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
            };
        }
    }
}
