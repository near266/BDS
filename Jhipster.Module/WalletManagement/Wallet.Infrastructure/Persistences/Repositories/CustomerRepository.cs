﻿using AutoMapper;
using Jhipster.Crosscutting.Utilities;
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
            var cusquery1 = query.OrderByDescending(i => i.CreatedDate);
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