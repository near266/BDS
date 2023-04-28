using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using Microsoft.EntityFrameworkCore;
using Post.Application.Contracts;
using Post.Domain.Abstractions;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Infrastructure.Persistences.Repositories
{

    public class PostRepository : IPostRepository
    {
        private readonly IPostDbContext _context;
        private readonly IMapper _mapper;
        public PostRepository(IPostDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddBoughtPost(BoughtPost rq, CancellationToken cancellationToken)
        {
            rq.Status = 0;
            await _context.BoughtPosts.AddAsync(rq);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> AddSalePost(SalePost rq, CancellationToken cancellationToken)
        {
            if (rq.Type == 0) rq.Status = 0;
            else rq.Status = 1;
            await _context.SalePosts.AddAsync(rq);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteBoughtPost(string Id, CancellationToken cancellationToken)
        {
            var check = await _context.BoughtPosts.FirstOrDefaultAsync(i => i.Id == Id);
            if (check == null) throw new ArgumentException("Can not find!");
            _context.BoughtPosts.Remove(check);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteSalePost(string Id, CancellationToken cancellationToken)
        {
            var check = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == Id);
            if (check == null) throw new ArgumentException("Can not find!");
            _context.SalePosts.Remove(check);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedList<BoughtPost>> SearchBoughtPost(int Page, int PageSize)
        {
            var value = new PagedList<BoughtPost>();
            var Data = await _context.BoughtPosts.ToListAsync();
            value.Data = Data.Skip(PageSize * (Page - 1))
                  .Take(PageSize);
            value.TotalCount = Data.Count;
            return value;
        }

        public async Task<PagedList<SalePost>> SearchSalePost(int Page, int PageSize)
        {
            var value = new PagedList<SalePost>();
            var Data = await _context.SalePosts.ToListAsync();
            value.Data = Data.Skip(PageSize * (Page - 1))
                  .Take(PageSize);
            value.TotalCount = Data.Count;
            return value;
        }

        public async Task<int> UpdateBoughtPost(BoughtPost rq, CancellationToken cancellationToken)
        {
            var check = await _context.BoughtPosts.FirstOrDefaultAsync(i=>i.Id==rq.Id);
            if (check == null) throw new ArgumentException("Can not find!");
            else
            {
                _mapper.Map(rq, check);
                return await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<int> UpdateSalePost(SalePost rq, CancellationToken cancellationToken)
        {
            var check = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == rq.Id);
            if (check == null) throw new ArgumentException("Can not find!");
            else
            {
                _mapper.Map(rq, check);
                return await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<BoughtPost> ViewDetailBoughtPost(string id)
        {
            var res = await _context.BoughtPosts.FirstOrDefaultAsync(i => i.Id == id);
            if(res == null) throw new ArgumentException("Can not find!");
            return res;
        }

        public async Task<SalePost> ViewDetailSalePost(string id)
        {
            var res = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == id);
            if (res == null) throw new ArgumentException("Can not find!");
            return res;
        }
    }
}
