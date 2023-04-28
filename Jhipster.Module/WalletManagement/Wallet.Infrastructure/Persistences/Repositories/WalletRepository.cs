﻿using AutoMapper;
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
    public class WalletRepository : IWalletRepository
    {
        private readonly IWalletDbContext _context;
        private readonly IMapper _mapper;
        public WalletRepository(IWalletDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Add(WalletEntity Wallet,CancellationToken cancellationToken)
        {
            await _context.Wallets.AddAsync(Wallet);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(Guid Id,CancellationToken cancellation)
        {
            var obj = await _context.Wallets.FirstOrDefaultAsync(i=>i.Id.Equals(Id));
            if (obj != null)
            {
                _context.Wallets.Remove(obj);
                return await _context.SaveChangesAsync(cancellation);
            }
            return 0;
        }

        public async Task<IEnumerable<WalletEntity>> GetAll()
        {
            var obj = await _context.Wallets.ToListAsync();

            return obj;
        }

        public async Task<int> Update(WalletEntity Wallet, CancellationToken cancellation)
        {
            var old = await _context.Wallets.FirstOrDefaultAsync(x => x.Id.Equals(Wallet.Id));
            if (old != null)
            {
                old = _mapper.Map<WalletEntity, WalletEntity>(Wallet, old);
                return await _context.SaveChangesAsync(cancellation);
            }
            return 0;
        }
    }
}
