using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using Jhipster.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Entities;

namespace Wallet.Infrastructure.Persistences.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IWalletDbContext _context;
        private readonly IMapper _mapper;
        private readonly ApplicationDatabaseContext _appcontext;

        public CustomerRepository(IWalletDbContext context, IMapper mapper, ApplicationDatabaseContext appcontext)
        {
            _context = context;
            _mapper = mapper;
            _appcontext = appcontext;
        }
        public async Task<int> Add(Customer cus, CancellationToken cancellationToken)
        {
            await _context.Customers.AddAsync(cus);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> Delete(List<Guid> Id, CancellationToken cancellationToken)
        {
            var check = await _context.Customers.Where(i => Id.Contains(i.Id)).ToListAsync();
            foreach (var cus in check)
            {
                cus.Status = true;
                var user = await _appcontext.Users.FirstOrDefaultAsync(i => i.Id == cus.Id.ToString());
                if (user == null) continue;
                user.Activated = false;
                await _appcontext.SaveChangesAsync(cancellationToken);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Customer> GetById(Guid Id)
        {
            var check = await _context.Customers.FirstOrDefaultAsync(i => i.Id == Id);
            if (check == null) throw new ArgumentException("Not exists!");
            return check;
        }

        public async Task<SearchCustomerReponse> Search(string? keyword, string? phone, bool? isUnique, int page, int pagesize)
        {
             var query = _context.Customers.AsQueryable();
            var listW = new List<WalletEntity>();
            var listWP = new List<WalletPromotional>();
            if (keyword != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.CustomerName) && i.CustomerName.ToLower().Contains(keyword.ToLower().Trim()));
            }
            if(phone != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Phone) && i.Phone.ToLower().Contains(phone.ToLower().Trim()));
            }
            if(isUnique != null)
            {
                if(isUnique == true)
                {
                    query = query.Where(i => i.IsUnique == true);
                }
                else
                {
                    query = query.Where(i => i.IsUnique == false);
                }
            }
            var cusquery1 = query.Where(i => i.Status == false).OrderByDescending(i => i.CreatedDate);
            var cusquery2 = await cusquery1.Skip(pagesize * (page - 1))
                                .Take(pagesize)
                                .ToListAsync();

            var relist = await cusquery1.ToListAsync();
            var listCusid = cusquery2.Select(i => i.Id).ToList();
            var tempRes = listCusid.Select(item => new CustomerDetail
            {
                customer = _context.Customers.FirstOrDefault(i => i.Id == item),
                wallet = _mapper.Map<WalletDto>(_context.Wallets.FirstOrDefault(i => i.CustomerId == item)),
                walletPromotional = _mapper.Map<WalletPromotionalDto>(_context.WalletPromotionals.FirstOrDefault(i => i.CustomerId == item))
            }).ToList();
            var res = new SearchCustomerReponse
            {
                Data = tempRes,
                TotalCount = relist.Count,
            };
            return res;
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
