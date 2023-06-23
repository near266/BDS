using AutoMapper;
using Jhipster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistences.Repositories
{
    public class TypePriceRepository : ITypePriceRepository
    {
        private readonly IWalletDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationDatabaseContext _appcontext;
        public TypePriceRepository(IWalletDbContext context, IMapper mapper, ApplicationDatabaseContext appcontext)
        {
            _context = context;
            _mapper = mapper;
            _appcontext = appcontext;
        }
        public async Task<int> Add(TypePrice typePrice, CancellationToken cancellationToken)
        {
            await _context.TypePrices.AddAsync(typePrice);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(List<Guid> Id, CancellationToken cancellationToken)
        {
            var check = await _context.TypePrices.Where(i => Id.Contains(i.Id)).ToListAsync();
            foreach (var item in check)
            {
                _context.TypePrices.Remove(item);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TypePrice>> GetAll()
        {
            var list = await _context.TypePrices.ToListAsync();
            return list;
        }

        public async Task<ViewDetailPriceDTO> GetPrice(Guid Id)
        {
            var list = await _context.TypePrices
                                .FirstOrDefaultAsync(i => i.Id == Id);
            if (list == null) throw new Exception("Fail");
            var value = await _context.PriceConfigurations.Where(i => i.TypePriceId == Id).GroupBy(i => i.Type).Select(a => new PriceConfigDTO
            {
                Type = a.Key,
                PriceConfig = _mapper.Map<List<PriceTypeDTO>>(a.ToList()),

            }).ToListAsync();
            var s = new ViewDetailPriceDTO()
            {
                TypePrice = list,
                PriceConfiguration = value
            };
            return s;
        }
        public async Task<int> Update(TypePrice typePrice, CancellationToken cancellationToken)
        {
            var check = await _context.TypePrices.FirstOrDefaultAsync(i => i.Id == typePrice.Id);
            if (check == null) throw new ArgumentException("Can not find");
            else
            {
                _mapper.Map(typePrice, check);
                return await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
