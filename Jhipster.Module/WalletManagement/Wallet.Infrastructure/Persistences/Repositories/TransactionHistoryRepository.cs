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
            if (rq.TransactionAmount != 0)
            {
                rq.CreatedDate = DateTime.Now;
                await _context.TransactionHistorys.AddAsync(rq, cancellationToken);
                return await _context.SaveChangesAsync(cancellationToken);
            }
            return 0;
        }

        public async Task<PagedList<SearchTransactionResponse>> Search(string? Code, string? userid, int? type, DateTime? from, DateTime? to, int Page, int PageSize)
        {
            var query = _context.TransactionHistorys.AsQueryable();
            if (type != null)
            {
                query = query.Where(x => x.Type == type);
            }
            if (from != null && to != null)
            {
                query = query.Where(i => i.CreatedDate >= from && i.CreatedDate <= to);
            }
            if (userid != null)
            {
                query = query.Where(i => i.CustomerId == Guid.Parse(userid));
            }
            if (Code != null)
            {
                query = query.Where(i => i.TransactionCode == Code);
            }    
                var sQuery = query.Include(i => i.Customer).OrderByDescending(i => i.CreatedDate);
            var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                                .Take(PageSize)
                                .ToListAsync();
            var reslist = await sQuery.ToListAsync();

            //var listId = sQuery1.Select(i => i.CustomerId).ToList();
            var res = reslist.Select(item => new SearchTransactionResponse
            {
                TransactionHistorys = _mapper.Map<TransactionHistoryDTO>(_context.TransactionHistorys.FirstOrDefault(x => x.Id == item.Id)),
                wallet = _mapper.Map<WalletDto>(_context.Wallets.FirstOrDefault(i => i.CustomerId == item.CustomerId)),
                walletPromotional = _mapper.Map<WalletPromotionalDto>(_context.WalletPromotionals.FirstOrDefault(i => i.CustomerId == item.CustomerId)),
                WalletAmount = _context.Wallets.FirstOrDefault(i => i.CustomerId == item.CustomerId).Amount,
                WalletPromotion = _context.WalletPromotionals.FirstOrDefault(i => i.CustomerId == item.CustomerId).Amount,

            }).ToList();
            foreach (var item in res)
            {
                item.wallet.Amount = (decimal)item.TransactionHistorys.Amount;
                item.walletPromotional.Amount = (decimal)item.TransactionHistorys.Walletamount;
                item.TotalAmount = (decimal)item.WalletAmount + (decimal)item.WalletPromotion;
                item.Poin = item.TransactionHistorys.Customer.Point;
                if (item.TransactionHistorys.Type != 1)
                {
                    item.TransactionHistorys.Status = "Up";
                    if (item.TransactionHistorys.WalletType == 0)
                    {
                        item.wallet.IsVolatility = true;
                        item.wallet.Status = "Up";
                        item.walletPromotional.IsVolatility = false;
                    }
                    else if (item.TransactionHistorys.WalletType == 1)
                    {
                        item.walletPromotional.Status = "Up";
                        item.walletPromotional.IsVolatility = true;
                        item.wallet.IsVolatility = false;

                    }


                }
                else
                {
                    item.TransactionHistorys.Status = "Down";
                    if (item.TransactionHistorys.WalletType == 0)
                    {
                        item.wallet.IsVolatility = true;
                        item.wallet.Status = "Down";
                        item.walletPromotional.IsVolatility = false;
                    }
                    else if (item.TransactionHistorys.WalletType == 1)
                    {
                        item.walletPromotional.Status = "Down";
                        item.walletPromotional.IsVolatility = true;
                        item.wallet.IsVolatility = false;

                    }
                }

            }

            return new PagedList<SearchTransactionResponse>
            {
                Data = res.Skip(PageSize * (Page - 1))
                                .Take(PageSize),

                TotalCount = reslist.Count,
            };
        }
    }
}
