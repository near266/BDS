using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using Jhipster.Domain;
using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistences.Repositories
{
    public class TransactionHistoryRepository : ITransactionHistoryRepository
    {
        private readonly IWalletDbContext _context;
        private readonly IMapper _mapper;

        public TransactionHistoryRepository(IWalletDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(TransactionHistory rq, CancellationToken cancellationToken)
        {
            await _context.TransactionHistorys.AddAsync(rq, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedList<SearchTransactionResponse>> Search(string? userid, int? type, DateTime? from, DateTime? to, int Page, int PageSize)
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
                
            var listId = sQuery1.Select(i => i.CustomerId).ToList();
            var res = listId.Select(item => new SearchTransactionResponse
            {
                TransactionHistory = _context.TransactionHistorys.FirstOrDefault(x => x.CustomerId == item),
                wallet = _mapper.Map<WalletDto>(_context.Wallets.FirstOrDefault(i => i.CustomerId == item)),
                walletPromotional = _mapper.Map<WalletPromotionalDto>(_context.WalletPromotionals.FirstOrDefault(i => i.CustomerId == item))
            }).ToList();


            return new PagedList<SearchTransactionResponse>
            {
                Data = res,
                TotalCount = reslist.Count,
            };
        }
    }
}
