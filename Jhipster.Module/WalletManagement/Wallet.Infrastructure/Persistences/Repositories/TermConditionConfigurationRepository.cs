using AutoMapper;
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
    public class TermConditionConfigurationRepository : ITermConditionConfigurationRepository
    {
        private readonly IWalletDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationDatabaseContext _appcontext;
        public TermConditionConfigurationRepository(IWalletDbContext context, IMapper mapper, ApplicationDatabaseContext appcontext)
        {
            _context = context;
            _mapper = mapper;
            _appcontext = appcontext;
        }
        public async Task<int> Add(TermConditionConfiguration rq, CancellationToken cancellationToken)
        {
            await _context.TermConditionConfigurations.AddAsync(rq);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(List<Guid> Id, CancellationToken cancellationToken)
        {
            var check = await _context.TermConditionConfigurations.Where(i => Id.Contains(i.Id)).ToListAsync();
            if (check == null) throw new ArgumentException("Can not find");
            foreach (var item in check)
            {
                _context.TermConditionConfigurations.Remove(item);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TermConditionConfiguration>> GetAll()
        {
            var list = await _context.TermConditionConfigurations.ToListAsync();
            return list;
        }

        public async Task<int> Update(TermConditionConfiguration rq, CancellationToken cancellationToken)
        {
            var check = await _context.TermConditionConfigurations.FirstOrDefaultAsync(i => i.Id == rq.Id);
            if (check == null) throw new ArgumentException("Can not find");
            else
            {
                _mapper.Map(rq, check);
                return await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<TermConditionConfiguration> ViewDetail(Guid Id)
        {
            var check = await _context.TermConditionConfigurations.Where(i => i.Id.Equals(Id)).Include(i=>i.TypeTerm).FirstOrDefaultAsync();
            return check;
        }
    }
}
