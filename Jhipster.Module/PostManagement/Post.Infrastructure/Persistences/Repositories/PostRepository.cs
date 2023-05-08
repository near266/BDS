﻿using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using Microsoft.EntityFrameworkCore;
using Post.Application.Contracts;
using Post.Domain.Abstractions;
using Post.Domain.Entities;
using Post.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Abstractions;

namespace Post.Infrastructure.Persistences.Repositories
{

    public class PostRepository : IPostRepository
    {
        private readonly IPostDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWalletDbContext _wcontext;

        public PostRepository(IPostDbContext context,IWalletDbContext wcontext, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _wcontext = wcontext;
        }

        public async Task<int> AddBoughtPost(BoughtPost rq, CancellationToken cancellationToken)
        {
            rq.Status = (int)PostStatus.UnApproved;
            await _context.BoughtPosts.AddAsync(rq);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> AddSalePost(SalePost rq, CancellationToken cancellationToken)
        {
            switch (rq.Type)
            {
                case (int)PostType.Normal:
                    rq.Status = (int)PostStatus.UnApproved;
                    break;
                case (int)PostType.Golden:
                    rq.Status = (int)PostStatus.Showing;
                    break;
                case (int)PostType.Vip:
                    rq.Status = (int)PostStatus.Showing;
                    break;
                default:
                    break;
            }
            await _context.SalePosts.AddAsync(rq);
            var res = await _context.SaveChangesAsync(cancellationToken);
            if(rq.Type == (int)PostType.Golden)
            {
                await SubtractMoney(rq.Id, 150000, cancellationToken);
            }else if(rq.Type == (int)PostType.Vip)
            {
                await SubtractMoney(rq.Id, 250000, cancellationToken);
            }
            return res;
        }

        public async Task<int> ApprovePost(int postType, string id,  int status, string? reason, DateTime? modifiedDate, string? modifiedBy, CancellationToken cancellationToken)
        {
            if (postType == 0)
            {
                var res = await _context.BoughtPosts.FirstOrDefaultAsync(i => i.Id == id);
                if (res == null) throw new ArgumentException("Not exists !");

                res.Status = status;
                res.Reason = reason;
                res.LastModifiedDate = modifiedDate;
                res.LastModifiedBy = modifiedBy;
                return await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                var res = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == id);
                if (res == null) throw new ArgumentException("Not exists !");
                res.Status = status;
                res.Reason = reason;
                res.LastModifiedDate = modifiedDate;
                res.LastModifiedBy = modifiedBy;
                var result = await _context.SaveChangesAsync(cancellationToken);
                if(status == (int)PostStatus.Showing)
                {
                    await SubtractMoney(id, 2500, cancellationToken);
                }
                return result;
            }
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


        public async Task<PagedList<BoughtPost>> SearchBoughtPost(string? userid, int Page, int PageSize)
        {
            var value = new PagedList<BoughtPost>();
            if (userid != null)
            {
                var Data = await _context.BoughtPosts.Where(i => i.UserId == userid).ToListAsync();
                value.Data = Data.Skip(PageSize * (Page - 1))
                      .Take(PageSize);
                value.TotalCount = Data.Count;
            }
            else
            {
                var Data = await _context.BoughtPosts.ToListAsync();
                value.Data = Data.Skip(PageSize * (Page - 1))
                      .Take(PageSize);
                value.TotalCount = Data.Count;
            }
            return value;
        }
        public async Task<PagedList<BoughtPost>> GetShowingBoughtPost(int Page, int PageSize)
        {
            var value = new PagedList<BoughtPost>();
            var Data = await _context.BoughtPosts.Where(i => i.Status == (int)PostStatus.Showing).ToListAsync();
            value.Data = Data.Skip(PageSize * (Page - 1))
                  .Take(PageSize);
            value.TotalCount = Data.Count;
            return value;
        }

        public async Task<PagedList<SalePost>> SearchSalePost(string? userid, int Page, int PageSize)
        {
            var value = new PagedList<SalePost>();
            if (userid != null)
            {
                var Data = await _context.SalePosts.Where(i => i.UserId == userid).ToListAsync();
                value.Data = Data.Skip(PageSize * (Page - 1))
                      .Take(PageSize);
                value.TotalCount = Data.Count;
            }
            else
            {
                var Data = await _context.SalePosts.ToListAsync();
                value.Data = Data.Skip(PageSize * (Page - 1))
                      .Take(PageSize);
                value.TotalCount = Data.Count;
            }

            return value;
        }

        public async Task<PagedList<SalePost>> GetShowingSalePost(int Page, int PageSize)
        {
            var value = new PagedList<SalePost>();
            var Data = await _context.SalePosts.Where(i => i.Status == (int)PostStatus.Showing).OrderByDescending(i => i.Type).ToListAsync();
            value.Data = Data.Skip(PageSize * (Page - 1))
                  .Take(PageSize);
            value.TotalCount = Data.Count;
            return value;
        }

        public async Task<int> UpdateBoughtPost(BoughtPost rq, CancellationToken cancellationToken)
        {
            var check = await _context.BoughtPosts.FirstOrDefaultAsync(i => i.Id == rq.Id);
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
            if (res == null) throw new ArgumentException("Can not find!");
            return res;
        }

        public async Task<SalePost> ViewDetailSalePost(string id)
        {
            var res = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == id);
            if (res == null) throw new ArgumentException("Can not find!");
            return res;
        }

        public async Task SubtractMoney(string? postid, decimal amount, CancellationToken cancellationToken)
        {
            if(postid  != null)
            {
                var post = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == postid);
                if (post == null) throw new ArgumentException("No post found !!!");
                var user = await _wcontext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == post.UserId);
                if (user == null) throw new ArgumentException("No user found !!!");
                user.Amount -= amount;
                user.LastModifiedDate = DateTime.UtcNow;
            }
            await _wcontext.SaveChangesAsync(cancellationToken);
        }
    }
}
