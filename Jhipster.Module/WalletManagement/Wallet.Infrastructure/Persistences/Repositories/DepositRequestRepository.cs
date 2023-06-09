using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using Jhipster.Infrastructure.Data;
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
    public class DepositRequestRepository : IDepositRequestRepository
    {
        private readonly IWalletDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationDatabaseContext _appcontext;
        public DepositRequestRepository(IWalletDbContext context, IMapper mapper, ApplicationDatabaseContext appcontext)
        {
            _context = context;
            _mapper = mapper;
            _appcontext = appcontext;
        }

        public async Task<int> Add(DepositRequest rq)
        {
            await _appcontext.DepositRequests.AddAsync(rq);
            return await _appcontext.SaveChangesAsync();
        }

        public async Task<PagedList<DepositRequest>> GetByAdmin(int Page, int PageSize)
        {
            var value = new PagedList<DepositRequest>();
            var data = await _context.DepositRequests
                             .Include(i => i.Customer).ToListAsync();
            value.TotalCount = data.Count;
            value.Data = data.Skip(PageSize * (Page - 1))
                                .Take(PageSize)
                                .ToList();
            return value;
        }

        public async Task<List<DepositRequest>> GetByUser(Guid Id)
        {
            return await _context.DepositRequests.Where(i => i.CustomerId == Id).ToListAsync();
        }

        public async Task<int> Update(Guid id, int status, CancellationToken cancellationToken)
        {
            var check = await _context.DepositRequests.FirstOrDefaultAsync(i => i.Id == id);
            if (check != null)
            {
                check.Status = status;
                return await _context.SaveChangesAsync(cancellationToken);
            }
            return 0;
        }
    }
}
