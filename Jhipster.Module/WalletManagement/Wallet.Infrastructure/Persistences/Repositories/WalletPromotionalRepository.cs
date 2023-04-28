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
        private readonly WalletDbContext _context;
        private readonly IMapper _mapper;
        WalletPromotionalRepository(WalletDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(WalletPromotional request)
        {   
            await  _context.WalletsPromotional.AddAsync(request);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Guid Id)
        {
            var check = await _context.WalletsPromotional.FirstOrDefaultAsync();
            if (check != null)
            {
                _context.Remove(check);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<WalletPromotional>> GetAll()
        {
            var obj = await _context.WalletsPromotional.ToListAsync();
            return obj;
        }

        public async Task<int> Update(WalletPromotional request)
        {
            var old = await _context.WalletsPromotional.FirstOrDefaultAsync(i=>i.Id.Equals(request.Id));
            if (old != null)
            {
                old = _mapper.Map<WalletPromotional,WalletPromotional>(request,old);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
