using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using Jhipster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
            if (priceConfiguration.Unit == 0)
            {
                priceConfiguration.Price = (decimal)(priceConfiguration.PriceDefault - priceConfiguration.Discount);

            }
            if (priceConfiguration.Unit == 1)
            {
                priceConfiguration.Price = (decimal)(priceConfiguration.PriceDefault * (100 - priceConfiguration.Discount) / 100);
            }
            await _context.PriceConfigurations.AddAsync(priceConfiguration);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(List<Guid> Id, CancellationToken cancellationToken)
        {
            var check = await _context.PriceConfigurations.Where(i => Id.Contains(i.Id)).ToListAsync();
            if (check == null) throw new ArgumentException("Can not find");
            foreach (var item in check)
            {
                _context.PriceConfigurations.Remove(item);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }



        public async Task<int> Update(PriceConfiguration priceConfiguration, CancellationToken cancellationToken)
        {
            var check = await _context.PriceConfigurations.FirstOrDefaultAsync(i => i.Id == priceConfiguration.Id);
            if (check == null) throw new ArgumentException("Can not find");
            else
            {
                _mapper.Map(priceConfiguration, check);
                return await _context.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task<IEnumerable<PriceConfiguration>> GetAll()
        {
            var list = await _context.PriceConfigurations.ToListAsync();
            return list;
        }

    }
}
