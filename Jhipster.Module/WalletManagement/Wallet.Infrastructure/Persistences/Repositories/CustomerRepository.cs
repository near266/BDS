using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IWalletDbContext _context;
        private readonly IMapper _mapper;
        public CustomerRepository(IWalletDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Add(Customer cus, CancellationToken cancellationToken)
        {
            await _context.Customers.AddAsync(cus);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(Guid Id, CancellationToken cancellationToken)
        {
            var check = await _context.Customers.FirstOrDefaultAsync(i => i.Id == Id);
            if (check == null) throw new ArgumentException("Not exists!");
            _context.Customers.Remove(check);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Customer> GetById(Guid Id)
        {
            var check = await _context.Customers.FirstOrDefaultAsync(i => i.Id == Id);
            if (check == null) throw new ArgumentException("Not exists!");
            return check;
        }

        public async Task<PagedList<Customer>> Search(int page, int pagesize)
        {
            var list = new PagedList<Customer>();
            var query = _context.Customers.AsQueryable();
            var query1 = await query.ToListAsync();
            var query2 = await query.Skip(pagesize * (page - 1))
                 .Take(pagesize).ToListAsync();
            return new PagedList<Customer>
            {
                Data = query2,
                TotalCount = query1.Count
            };
        }

        public async Task<int> Update(Customer cus, CancellationToken cancellationToken)
        {
            var res = await _context.Customers.FirstOrDefaultAsync(u => u.Id == cus.Id);
            if (res == null) throw new ArgumentException("Customer not found");
            _mapper.Map(cus, res);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
