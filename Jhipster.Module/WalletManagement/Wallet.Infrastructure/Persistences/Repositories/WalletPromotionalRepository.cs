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
            var check = await _context.WalletPromotionals.FirstOrDefaultAsync();
            if (check != null)
            {
                _context.WalletPromotionals.Remove(check);
                return await _context.SaveChangesAsync(cancellationToken);
            }
            return 0;
        }

        public async Task<IEnumerable<WalletPromotional>> GetAll()
        {
            var obj = await _context.WalletPromotionals.ToListAsync();
            return obj;
        }

        public async Task<int> Update(WalletPromotional request, CancellationToken cancellationToken)
        {
            var old = await _context.WalletPromotionals.FirstOrDefaultAsync(i => i.Id.Equals(request.Id));
            if (old != null)
            {
                old = _mapper.Map<WalletPromotional, WalletPromotional>(request, old);
                return await _context.SaveChangesAsync(cancellationToken);
            }
            return 0;
        }
    }
}
