using AutoMapper;
using Jhipster.Infrastructure.Data;
using Jhipster.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistences.Repositories
{
    public class TypeTermRepository : ITypeTermRepository
    {
        private readonly IWalletDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationDatabaseContext _appcontext;
        public TypeTermRepository(IWalletDbContext context, IMapper mapper, ApplicationDatabaseContext appcontext)
        {
            _context = context;
            _mapper = mapper;
            _appcontext = appcontext;
        }

        public async Task<int> AddTypeTerm(TypeTerm typeTerm , CancellationToken cancellationToken)
        {
             _context.TypeTerms.Add(typeTerm);
             
           return await _context.SaveChangesAsync(cancellationToken); ;
        }

        public async Task<IEnumerable<TypeTerm>> GetAll()
        {
            var list = await _context.TypeTerms.Include(i=>i.TermConfig).AsNoTracking().IgnoreAutoIncludes().ToListAsync();
            return list;
        }

        public async Task<TypeTerm> GetById(Guid? Id)
        {
            var check =await _context.TypeTerms.Where(i=>i.Id.Equals(Id)).Include(i=>i.TermConfig).AsNoTracking().IgnoreAutoIncludes()
                .FirstOrDefaultAsync();

            return check;
        }
    }
}
