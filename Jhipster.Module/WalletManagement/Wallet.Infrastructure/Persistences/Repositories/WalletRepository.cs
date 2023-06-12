using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistences.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IWalletDbContext _context;
        private readonly IMapper _mapper;
        public WalletRepository(IWalletDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(WalletEntity Wallet, CancellationToken cancellationToken)
        {
            await _context.Wallets.AddAsync(Wallet);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(Guid Id, CancellationToken cancellation)
        {
            var obj = await _context.Wallets.FirstOrDefaultAsync(i => i.Id == Id);
            if(obj == null ) throw new ArgumentException("wallet not found");
            _context.Wallets.Remove(obj);
            return await _context.SaveChangesAsync(cancellation);
        }

        public async Task<IEnumerable<WalletEntity>> GetAll()
        {
            var obj = await _context.Wallets.ToListAsync();

            return obj;
        }

        public async Task<WalletResponseDTO> GetWalletByUserId(string? userId)
        {
            var w = await _context.Wallets.FirstOrDefaultAsync(i => i.CustomerId == Guid.Parse(userId));
            if (w == null) throw new ArgumentException("Wallet not found");
            var wp = await _context.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId == Guid.Parse(userId));
            if (wp == null) throw new ArgumentException("Wallet Promotional not found");
            var cus = await _context.Customers.FirstOrDefaultAsync(i => i.Id == Guid.Parse(userId));
            if (cus == null) throw new ArgumentException("Customer not found");
            var res = new WalletResponseDTO
            {
                wallet = _mapper.Map<WalletDto>(w),
                walletPromotional = _mapper.Map<WalletPromotionalDto>(wp),
                customer = new CustomerDto
                {
                    Point = cus.Point
                }
            };
            return res;
        }

        public async Task<int> Update(WalletEntity Wallet, CancellationToken cancellation)
        {
            var res = await _context.Wallets.FirstOrDefaultAsync(u => u.CustomerId == Wallet.CustomerId);
            if (res == null) throw new ArgumentException("wallet not found");
            res.Currency = "VND";
            res.CustomerId = Wallet.CustomerId;
            res.LastModifiedDate = Wallet.LastModifiedDate;
            res.LastModifiedBy = Wallet.LastModifiedBy;
            res.Username = "string";
            res.Amount = res.Amount == 0 ? Wallet.Amount : res.Amount + Wallet.Amount;

            var cus = await _context.Customers.FirstOrDefaultAsync(u => u.Id == res.CustomerId);
            if (cus == null) throw new ArgumentException("Customer not found");
            cus.Point = cus.Point == 0 ? (double?)(Wallet.Amount / 1000) : cus.Point + (double?)(Wallet.Amount / 1000);
            return await _context.SaveChangesAsync(cancellation);
        }
    }
}
