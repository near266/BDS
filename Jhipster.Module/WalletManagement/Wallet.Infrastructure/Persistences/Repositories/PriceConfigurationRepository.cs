using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using Jhipster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Wallet.Application.Persistences;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistences.Repositories
{
    public class PriceConfigurationRepository : IPriceConfigurationRepository
    {
        private readonly IWalletDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationDatabaseContext _appcontext;
        public PriceConfigurationRepository(IWalletDbContext context, IMapper mapper, ApplicationDatabaseContext appcontext)
        {
            _context = context;
            _mapper = mapper;
            _appcontext = appcontext;
        }
        public async Task<int> Add(PriceConfiguration priceConfiguration, CancellationToken cancellationToken)
        {
            await _context.PriceConfigurations.AddAsync(priceConfiguration);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(List<Guid> Id, CancellationToken cancellationToken)
        {
            var check  = await _context.PriceConfigurations.Where(i => Id.Contains(i.Id)).ToListAsync();
            if (check == null) throw new ArgumentException("Can not find");
            foreach(var item in check)
            {
                _context.PriceConfigurations.Remove(item);
            }
            return await _context.SaveChangesAsync(cancellationToken);

        }   



        public async Task<List<PriceConfiguration>> GetPriceConfigurationByTypePriceId(Guid TypePriceId)
        {
            var query = _context.PriceConfigurations.AsQueryable();

            if (TypePriceId != null)
            {
                query = query.Where(i => i.TypePriceId == TypePriceId);
            }
            var sQuery = query.Include(i => i.TypePrice).OrderBy(i => i.CreatedDate);
            var reslist = await sQuery.ToListAsync();
            return reslist;

        }

        public Task<int> Update(PriceConfiguration priceConfiguration, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
