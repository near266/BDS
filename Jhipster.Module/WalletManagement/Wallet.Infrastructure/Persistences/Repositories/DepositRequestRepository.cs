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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<PagedList<DepositRequest>> GetByAdmin(int Page, int PageSize, string? UserName, DateTime? createDate, DateTime? DateTo, DateTime? DateFrom)
        {
            var value = new PagedList<DepositRequest>();
            var data = _context.DepositRequests
                             .Include(i => i.Customer).AsQueryable();
            if (UserName != null)
            {
                data = data.Where(i => i.Customer.CustomerName.Contains(UserName));
            }
            if (createDate != null)
            {
				var StartCreateDate = new DateTime(createDate.Value.Year, createDate.Value.Month, createDate.Value.Day  , 0, 0, 0);
				var EndCreateDate = new DateTime(createDate.Value.Year, createDate.Value.Month, createDate.Value.Day  , 23, 59, 59);
				data = data.Where(i => i.CreatedDate >= StartCreateDate && i.CreatedDate <= EndCreateDate);
			}
            if (DateFrom != null && DateTo != null)
            {
                data = data.Where(i => i.CreatedDate >= DateFrom && i.CreatedDate <= DateTo);
            }
            var query = data.OrderByDescending(i => i.CreatedDate);
            var query1 = await data.Skip(PageSize * (Page - 1))
                                .Take(PageSize)
                                .ToListAsync();
            value.TotalCount = query.Count();
            value.Data = query1;
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
