using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Post.Application.Contracts;
using Post.Application.DTO;
using Post.Domain.Abstractions;
using Post.Domain.Entities;
using Post.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Post.Infrastructure.Persistences.Repositories
{

    public class PostRepository : IPostRepository
    {
        private readonly IPostDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWalletDbContext _wcontext;
        private readonly IConfiguration _configuration;


        public PostRepository(IPostDbContext context, IWalletDbContext wcontext, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _wcontext = wcontext;
            _configuration = configuration;
        }

        #region BoughtPost
        public async Task<List<BoughtPost>> GetRandomBoughtPost(int randomCount, string? region)
        {
            var value = await _context.BoughtPosts.ToListAsync();

            if (!string.IsNullOrEmpty(region))
            {
                value = value.Where(i => i.Region.Contains(region)).ToList();
            }

            var random = new Random();
            int n = value.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                BoughtPost temp = value[i];
                value[i] = value[j];
                value[j] = temp;
            }

            return value.Take(randomCount).ToList();
        }
        public async Task<int> AddBoughtPost(BoughtPost rq, CancellationToken cancellationToken)
        {
            rq.Status = (int)PostStatus.UnApproved;
            await _context.BoughtPosts.AddAsync(rq);
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> DeleteBoughtPost(List<string> Id, CancellationToken cancellationToken)
        {
            var check = await _context.BoughtPosts.Where(i => Id.Contains(i.Id)).ToListAsync();
            foreach (var item in check)
            {
                _context.BoughtPosts.Remove(item);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<PagedList<BoughtPost>> SearchBoughtPost(string? userid, string? title, int? status, int Page, int PageSize)
        {
            var query = _context.BoughtPosts.AsQueryable();

            if (title != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Titile) && i.Titile.ToLower().Contains(title.ToLower().Trim())
                                    || !string.IsNullOrEmpty(i.Id) && i.Id.ToLower().Contains(title.ToLower().Trim()));
            }

            if (status != null)
            {
                query = query.Where(i => i.Status == status);
            }

            if (userid != null)
            {
                var sQuery = query.Where(i => i.UserId == userid).OrderByDescending(i => i.CreatedDate);
                var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                                    .Take(PageSize)
                                    .ToListAsync();
                var reslist = await sQuery.ToListAsync();
                return new PagedList<BoughtPost>
                {
                    Data = sQuery1,
                    TotalCount = reslist.Count,
                };
            }
            else
            {
                var sQuery = query.OrderByDescending(i => i.CreatedDate);
                var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                                    .Take(PageSize)
                                    .ToListAsync();
                var reslist = await sQuery.ToListAsync();
                return new PagedList<BoughtPost>
                {
                    Data = sQuery1,
                    TotalCount = reslist.Count,
                };
            }
        }
        public async Task<PagedList<BoughtPost>> GetShowingBoughtPost(string? userid, string? keyword, int? fromPrice, int? toPrice, string? region, int Page, int PageSize)
        {
            var query = _context.BoughtPosts.AsQueryable();

            if (keyword != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Titile) && i.Titile.ToLower().Contains(keyword.ToLower().Trim()));
            }

            if (fromPrice >= 0)
            {
                if (toPrice > 0)
                {
                    query = query.Where(i => i.Price > 0 && (i.Price >= fromPrice && i.Price <= toPrice));
                }
                else
                {
                    query = query.Where(i => i.Price > 0 && i.Price >= fromPrice);
                }
            }


            if (region != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Region) && i.Region.ToLower().Contains(region.ToLower().Trim()));
            }

            if (userid != null)
            {
                query = query.Where(i => i.UserId == userid);
            }

            var sQuery = query.Where(i => i.Status == (int)PostStatus.Showing).OrderByDescending(i => i.CreatedDate);
            var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                                .Take(PageSize)
                                .ToListAsync();

            var reslist = await sQuery.ToListAsync();
            return new PagedList<BoughtPost>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
            };
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
        public async Task<BoughtPost> ViewDetailBoughtPost(string id)
        {
            var res = await _context.BoughtPosts.FirstOrDefaultAsync(i => i.Id == id);
            if (res == null) throw new ArgumentException("Can not find!");
            return res;
        }
        #endregion

        #region SalePost
        public async Task<List<SalePost>> GetRandomSalePost(int randomCount, string? region)
        {
            var value = await _context.SalePosts.ToListAsync();

            if (!string.IsNullOrEmpty(region))
            {
                value = value.Where(i => i.Region.Contains(region)).ToList();
            }

            var random = new Random();
            int n = value.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                SalePost temp = value[i];
                value[i] = value[j];
                value[j] = temp;
            }

            return value.Take(randomCount).ToList();
        }

        public async Task<int> AddSalePost(SalePost rq, bool? isEnoughWallet, bool? isEnoughWalletPro, double numofDate, CancellationToken cancellationToken)
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
            if (rq.Type == (int)PostType.Normal)
            {
                if (isEnoughWalletPro == true)
                {
                    await SubtractMoneyPromotional(rq.Id, (decimal)(_configuration.GetValue<int>("Price:Normal") * numofDate), cancellationToken);
                }
                else if (isEnoughWalletPro == false && isEnoughWallet == true)
                {
                    await SubtractMoney(rq.Id, (decimal)(_configuration.GetValue<int>("Price:Normal") * numofDate), cancellationToken);
                }
            }
            else if (rq.Type == (int)PostType.Golden)
            {
                if (isEnoughWalletPro == true)
                {
                    await SubtractMoneyPromotional(rq.Id, (decimal)(_configuration.GetValue<int>("Price:Vip") * numofDate), cancellationToken);
                }
                else if (isEnoughWalletPro == false && isEnoughWallet == true)
                {
                    await SubtractMoney(rq.Id, (decimal)(_configuration.GetValue<int>("Price:Vip") * numofDate), cancellationToken);
                }
            }
            else if (rq.Type == (int)PostType.Vip)
            {
                if (isEnoughWalletPro == true)
                {
                    await SubtractMoneyPromotional(rq.Id, (decimal)(_configuration.GetValue<int>("Price:SuperVip") * numofDate), cancellationToken);
                }
                else if (isEnoughWalletPro == false && isEnoughWallet == true)
                {
                    await SubtractMoney(rq.Id, (decimal)(_configuration.GetValue<int>("Price:SuperVip") * numofDate), cancellationToken);
                }
            }
            return res;
        }
        public async Task<int> DeleteSalePost(List<string> Id, CancellationToken cancellationToken)
        {
            var check = await _context.SalePosts.Where(i => Id.Contains(i.Id)).ToListAsync();
            foreach (var item in check)
            {
                _context.SalePosts.Remove(item);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<PagedList<SalePost>> SearchSalePost(string? userid, string? title, int? status, int? type,
            DateTime? fromDate, DateTime? toDate, string? sortFeild, bool? sortValue, int Page, int PageSize)
        {
            var query = _context.SalePosts.AsQueryable();

            if (title != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Titile) && i.Titile.ToLower().Contains(title.ToLower().Trim())
                                     || !string.IsNullOrEmpty(i.Id) && i.Id.ToLower().Contains(title.ToLower().Trim()));
            }

            if (status != null)
            {
                query = query.Where(i => i.Status == status);
            }

            if (type != null)
            {
                query = query.Where(i => i.Type == type);
            }

            if (fromDate != null && toDate != null)
            {
                query = query.Where(i => i.CreatedDate >= fromDate && i.CreatedDate <= toDate);
            }


            if (!string.IsNullOrEmpty(sortFeild))
            {
                if (sortValue == true)
                {
                    query = query.OrderByDescending(i => EF.Property<object>(i, sortFeild));
                }
                else
                {
                    query = query.OrderBy(i => EF.Property<object>(i, sortFeild));
                }

                if (userid != null)
                {
                    var sQuery = query.Where(i => i.UserId == userid);
                    var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                                        .Take(PageSize)
                                        .ToListAsync();
                    var reslist = await sQuery.ToListAsync();
                    return new PagedList<SalePost>
                    {
                        Data = sQuery1,
                        TotalCount = reslist.Count,
                    };
                }
                else
                {
                    var sQuery = query;
                    var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                                        .Take(PageSize)
                                        .ToListAsync();
                    var reslist = await sQuery.ToListAsync();
                    return new PagedList<SalePost>
                    {
                        Data = sQuery1,
                        TotalCount = reslist.Count,
                    };
                }
            }

            if (userid != null)
            {
                var sQuery = query.Where(i => i.UserId == userid).OrderByDescending(i => i.CreatedDate);
                var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                                    .Take(PageSize)
                                    .ToListAsync();
                var reslist = await sQuery.ToListAsync();
                return new PagedList<SalePost>
                {
                    Data = sQuery1,
                    TotalCount = reslist.Count,
                };
            }
            else
            {
                var sQuery = query.OrderByDescending(i => i.CreatedDate);
                var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                                    .Take(PageSize)
                                    .ToListAsync();
                var reslist = await sQuery.ToListAsync();
                return new PagedList<SalePost>
                {
                    Data = sQuery1,
                    TotalCount = reslist.Count,
                };
            }
        }
        public async Task<PagedList<SearchSalePostDTO>> GetShowingSalePost(string? userid, string? keyword, int? fromPrice, int? toPrice, double? fromArea, double? toArea,
            string? region, int Page, int PageSize)
        {
            var result = await _context.SalePosts.ToListAsync();
            var query = _mapper.Map<List<SearchSalePostDTO>>(result).AsEnumerable();
            foreach (var item in query)
            {
                item.MaxSale = MaxSalePost(item);
                item.MinSale = MinSalePost(item);
            }
            if (keyword != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Titile) && i.Titile.ToLower().Contains(keyword.ToLower().Trim()));
            }

            if (fromPrice >= 0)
            {
                if (toPrice > 0)
                {
                    query = query.Where(i => i.Price > 0 && (i.Price >= fromPrice && i.Price <= toPrice));
                }
                else
                {
                    query = query.Where(i => i.Price > 0 && i.Price >= fromPrice);
                }
            }

            if (fromArea >= 0)
            {
                if (toArea > 0) query = query.Where(i => i.Area > 0 && (i.Area >= fromArea && i.Area <= toArea));
                else query = query.Where(i => i.Area > 0 && i.Area >= fromArea);
            }

            if (region != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Region) && i.Region.ToLower().Contains(region.ToLower().Trim()));
            }

            if (userid != null)
            {
                query = query.Where(i => i.UserId == userid);
            }

            var sQuery = query.Where(i => i.Status == (int)PostStatus.Showing)
                .OrderByDescending(i => i.Type).ThenByDescending(i => i.CreatedDate);
            var sQuery1 = sQuery.Skip(PageSize * (Page - 1))
                                .Take(PageSize)
                                .ToList();

            var reslist = sQuery.ToList();
            return new PagedList<SearchSalePostDTO>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
            };
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

        public async Task<SalePost> ViewDetailSalePost(string id)
        {
            var res = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == id);
            if (res == null) throw new ArgumentException("Can not find!");
            return res;
        }
        #endregion

        #region Other
        public async Task<bool> CheckTitle(string title, string userid)
        {
            var post = await _context.BoughtPosts.Where(i => i.UserId == userid).AsNoTracking().ToListAsync();
            foreach (var item in post.Select(i => i.Titile))
            {
                if (!string.IsNullOrEmpty(item) && title.Equals(item))
                {
                    return false;
                }
            }
            return true;
        }
        public async Task<int> ApprovePost(int postType, List<string> id, int status, string? reason, DateTime? modifiedDate, string? modifiedBy, CancellationToken cancellationToken)
        {
            if (postType == 0)
            {
                var res = await _context.BoughtPosts.Where(i => id.Contains(i.Id)).ToListAsync();
                foreach (var item in res)
                {
                    item.Status = status;
                    item.Reason = reason;
                    item.LastModifiedDate = modifiedDate;
                    item.LastModifiedBy = modifiedBy;
                }

                return await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                var res = await _context.SalePosts.Where(i => id.Contains(i.Id)).ToListAsync();
                foreach (var item in res)
                {
                    item.Status = status;
                    item.Reason = reason;
                    item.LastModifiedDate = modifiedDate;
                    item.LastModifiedBy = modifiedBy;
                }
                var result = await _context.SaveChangesAsync(cancellationToken);
                foreach (var item2 in res)
                {
                    if (status == (int)PostStatus.Rejected)
                    {
                        var check = await CheckBalancePromotional(item2.UserId, (int)PostType.Normal);
                        var dif = (item2.DueDate - item2.CreatedDate).Value.TotalDays;
                        if (check)
                        {
                            await ReturnMoney(item2.Id, (decimal)(_configuration.GetValue<int>("Price:Normal") * dif), 0, cancellationToken);
                        }
                        else
                        {
                            await ReturnMoney(item2.Id, (decimal)(_configuration.GetValue<int>("Price:Normal") * dif), 1, cancellationToken);
                        }
                    }
                }
                return result;
            }
        }
        public async Task ReturnMoney(string? postid, decimal amount, int type, CancellationToken cancellationToken)
        {
            if (postid != null)
            {
                var post = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == postid);
                if (post == null) throw new ArgumentException("No post found !!!");
                if (type == 0)
                {
                    var user = await _wcontext.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId.ToString() == post.UserId);
                    if (user == null) throw new ArgumentException("No user found !!!");
                    user.Amount += amount;
                    user.LastModifiedDate = DateTime.UtcNow;
                    await _wcontext.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    var user = await _wcontext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == post.UserId);
                    if (user == null) throw new ArgumentException("No user found !!!");
                    user.Amount += amount;
                    user.LastModifiedDate = DateTime.UtcNow;
                    await _wcontext.SaveChangesAsync(cancellationToken);
                }

            }
        }
        public async Task SubtractMoney(string? postid, decimal amount, CancellationToken cancellationToken)
        {
            if (postid != null)
            {
                var post = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == postid);
                if (post == null) throw new ArgumentException("No post found !!!");
                var user = await _wcontext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == post.UserId);
                if (user == null) throw new ArgumentException("No user found !!!");
                user.Amount -= amount;
                user.LastModifiedDate = DateTime.UtcNow;
                await _wcontext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task<bool> CheckBalance(string userId, int type)
        {
            var check = false;
            var user = await _wcontext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == userId);
            if (user == null) throw new ArgumentException("User Not Found !!!");
            var normalPrice = _configuration.GetValue<int>("Price:Normal");
            var vipPrice = _configuration.GetValue<int>("Price:Vip");
            var superVipPrice = _configuration.GetValue<int>("Price:SuperVip");

            if (type == (int)PostType.Normal && user.Amount > 0 && user.Amount > normalPrice)
            {
                check = true;
            }
            else if (type == (int)PostType.Golden && user.Amount > 0 && user.Amount > vipPrice)
            {
                check = true;
            }
            else if (type == (int)PostType.Vip && user.Amount > 0 && user.Amount > superVipPrice)
            {
                check = true;
            }
            return check;
        }
        public async Task SubtractMoneyPromotional(string? postid, decimal amount, CancellationToken cancellationToken)
        {
            if (postid != null)
            {
                var post = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == postid);
                if (post == null) throw new ArgumentException("No post found !!!");
                var user = await _wcontext.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId.ToString() == post.UserId);
                if (user == null) throw new ArgumentException("No user found !!!");
                user.Amount -= amount;
                user.LastModifiedDate = DateTime.UtcNow;
                await _wcontext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task<bool> CheckBalancePromotional(string userId, int type)
        {
            var check = false;
            var user = await _wcontext.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId.ToString() == userId);
            if (user == null) throw new ArgumentException("User Not Found !!!");

            var normalPrice = _configuration.GetValue<int>("Price:Normal");
            var vipPrice = _configuration.GetValue<int>("Price:Vip");
            var superVipPrice = _configuration.GetValue<int>("Price:SuperVip");

            if (type == (int)PostType.Normal && user.Amount > 0 && user.Amount > normalPrice)
            {
                check = true;
            }
            else if (type == (int)PostType.Golden && user.Amount > 0 && user.Amount > vipPrice)
            {
                check = true;
            }
            else if (type == (int)PostType.Vip && user.Amount > 0 && user.Amount > superVipPrice)
            {
                check = true;
            }
            return check;
        }
        public async Task<List<PostDto>> GetAllRegion(int? type)
        {
            if (type == 0)
            {
                var regionsBought = await _context.BoughtPosts.Where(i => i.Status == (int)PostStatus.Showing)
                .GroupBy(p => p.Region)
                .Select(g => new PostDto { Region = g.Key ?? "Unknown", Count = g.Count() })
                .ToListAsync();
                return regionsBought;
            }
            else
            {
                var regionsSale = await _context.SalePosts.Where(i => i.Status == (int)PostStatus.Showing)
                .GroupBy(p => p.Region)
                .Select(g => new PostDto { Region = g.Key ?? "Unknown", Count = g.Count() })
                .ToListAsync();
                return regionsSale;
            }
        }

        public async Task<List<StatusDto>> GetAllStatus(int? type, string userId)
        {
            if (type == 0)
            {
                var statusBought = await _context.BoughtPosts.Where(i => i.UserId == userId)
                .GroupBy(p => p.Status)
                .Select(g => new StatusDto { Status = g.Key, Count = g.Count() })
                .ToListAsync();
                return statusBought;
            }
            else
            {
                var statusSale = await _context.SalePosts.Where(i => i.UserId == userId)
                .GroupBy(p => p.Status)
                .Select(g => new StatusDto { Status = g.Key, Count = g.Count() })
                .ToListAsync();
                return statusSale;
            }
        }
        #endregion
        public string MaxSalePost(SearchSalePostDTO rq)
        {
            var check = _context.SalePosts.Where(i => i.Region == rq.Region).OrderBy(i => i.Price)
                .Select(i => i.Id).FirstOrDefault();
            if (check == rq.Id)
            {
                return "MaxSale";
            }
            else
            {
                return "NoMaxSale";
            }
        }

        public string MinSalePost(SearchSalePostDTO rq)
        {
            var check = _context.SalePosts.Where(i => i.Region == rq.Region).OrderByDescending(i => i.Price)
                .Select(i => i.Id).FirstOrDefault();
            if (check == rq.Id)
            {
                return "MinSale";
            }
            else
            {
                return "NoMinSale";
            }
        }
    }
}
