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
                cus.Status = false;
                var user = await _appcontext.Users.FirstOrDefaultAsync(i => i.Id == cus.Id.ToString());
                if (user == null) continue;
                user.Activated = false;
                await _appcontext.SaveChangesAsync(cancellationToken);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<DetailCusDTO> GetById(Guid Id)
        {
            var check = await _appcontext.Customers.FirstOrDefaultAsync(i => i.Id == Id);
            var mapStringId = Id.ToString();
            var checkUser = await _appcontext.Users.FirstOrDefaultAsync(i => i.Id == mapStringId);
            if (check == null) throw new ArgumentException("Not exists!");
            var map = _mapper.Map<DetailCusDTO>(check);
            var user = await _appcontext.Users.FirstOrDefaultAsync(i => i.Id == Id.ToString());
            map.TotalBoughtPost = _appcontext.BoughtPosts.Where(i => i.UserId == user.Id && i.Status == 1).Count();
            map.TotalSalePost = _appcontext.SalePosts.Where(i => i.UserId == user.Id && i.Status == 1).Count();
            map.ReferalCode = checkUser.ReferalCode;
            map.firstName = check.CustomerName;
            map.phoneNumber = checkUser.PhoneNumber;
            map.email = checkUser.Email;
            var IdToString = Id.ToString();
            map.Region = await _appcontext.SalePosts.Where(i => i.UserId == IdToString).Select(i => i.Region).ToListAsync();
            return map;
        }



        public async Task<SearchCustomerReponse> Search(string? CustomerCode,string? keyword, string? phone, bool? isUnique, int page, int pagesize)
        {
            var query = _context.Customers.AsQueryable();
            if(CustomerCode != null)
            {
                query=query.Where(i=>i.CustomerCode.Equals(CustomerCode));
            }
            
            if (keyword != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.CustomerName) && i.CustomerName.ToLower().Contains(keyword.ToLower().Trim()));
            }
            if (phone != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Phone) && i.Phone.ToLower().Contains(phone.ToLower().Trim()));
            }
            if (isUnique != null)
            {
                if (isUnique == true)
                {
                    query = query.Where(i => i.IsUnique == true);
                }
                else
                {
                    query = query.Where(i => i.IsUnique == false);
                }
            }
            var cusquery1 = query.Where(i => i.Status == true).OrderByDescending(i => i.CreatedDate);
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

        public async Task<string> GetMaxCode()
        {
            var maxCode = await _context.Customers.OrderByDescending(i => i.CustomerCode).Select(i => i.CustomerCode).FirstOrDefaultAsync();
            if (maxCode == "" || maxCode == null) maxCode = "HL000000";
            return maxCode;

        }
    }
}
