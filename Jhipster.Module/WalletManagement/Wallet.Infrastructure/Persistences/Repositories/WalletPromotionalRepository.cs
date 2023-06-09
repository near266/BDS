using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class WalletPromotionalRepository : IWalletPromotionalRepository
    {
        private readonly IWalletDbContext _context;
        private readonly IMapper _mapper;
        public WalletPromotionalRepository(IWalletDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(WalletPromotional request, CancellationToken cancellationToken)
        {
            await _context.WalletPromotionals.AddAsync(request);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(Guid Id, CancellationToken cancellationToken)
        {
            var check = await _context.WalletPromotionals.FirstOrDefaultAsync(i => i.Id == Id);
            if(check == null) throw new ArgumentException("WalletPromotional not found");
            _context.WalletPromotionals.Remove(check);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<WalletPromotional>> GetAll()
        {
            var obj = await _context.WalletPromotionals.ToListAsync();
            return obj;
        }

        public async Task<int> Update(WalletPromotional request, CancellationToken cancellationToken)
        {
            var res = await _context.WalletPromotionals.FirstOrDefaultAsync(u => u.CustomerId == request.CustomerId);
            if (res == null) throw new ArgumentException("walletPromotional not found");
            res.Currency = "VND";
            res.CustomerId = request.CustomerId;
            res.LastModifiedDate = request.LastModifiedDate;
            res.LastModifiedBy = request.LastModifiedBy;
            res.Username = "string";
            res.Amount = res.Amount == 0 ? request.Amount : res.Amount + request.Amount;
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
