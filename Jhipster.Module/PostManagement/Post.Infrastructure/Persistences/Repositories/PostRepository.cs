using AutoMapper;
using Jhipster.Crosscutting.Constants;
using Jhipster.Crosscutting.Utilities;
using Jhipster.Domain;
using Jhipster.Infrastructure.Data;
using JHipsterNet.Core.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Post.Application.Commands.SalePostC;
using Post.Application.Commands.WardC;
using Post.Application.Contracts;
using Post.Application.DTO;
using Post.Application.DTO.SalePostDtos;
using Post.Domain.Abstractions;
using Post.Domain.Entities;
using Post.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Entities;
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
        private readonly ApplicationDatabaseContext _databaseContext;
        public PostRepository(IPostDbContext context, IWalletDbContext wcontext, IMapper mapper, IConfiguration configuration, ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = context;
            _mapper = mapper;
            _wcontext = wcontext;
            _databaseContext = applicationDatabaseContext;
            _configuration = configuration;
        }

        #region BoughtPost
        public async Task<List<BoughtPost>> GetRandomBoughtPost(int randomCount, string? region)
        {
            var value = await _context.BoughtPosts.ToListAsync();

            if (!string.IsNullOrEmpty(region))
            {
                value = value.Where(i => i.Region.ToLower().Contains(region.ToLower())).ToList();
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
            rq.Order = DateTime.UtcNow;
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
        public async Task<PagedList<BoughtPost>> SearchBoughtPost(string? Id, string? userid, string? title, int? status, DateTime? fromDate, DateTime? toDate, int Page, int PageSize)
        {
            var query = _context.BoughtPosts.AsQueryable();
            if (!string.IsNullOrEmpty(Id))
            {
                query = query.Where(i => i.Id.Equals(Id));
            }

            if (title != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Titile) && i.Titile.ToLower().Contains(title.ToLower().Trim()));

            }

            if (status != null)
            {
                query = query.Where(i => i.Status == status);
            }

            if (fromDate != null && toDate != null)
            {
                query = query.Where(i => i.CreatedDate >= fromDate && i.CreatedDate <= toDate);
            }

            if (userid != null)
            {
                var sQuery = query.Where(i => i.UserId == userid).OrderByDescending(i => i.LastModifiedDate);
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
                var sQuery = query.OrderByDescending(i => i.LastModifiedDate);
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
        public async Task<PagedList<SearchBoughtPostDTO>> GetShowingBoughtPost(string? userid, string? keyword, double? fromPrice, double? toPrice, string? region, int Page, int PageSize)
        {
            var result = _databaseContext.BoughtPosts.AsQueryable();
            var query = _mapper.Map<List<SearchBoughtPostDTO>>(result).AsEnumerable();

            if (keyword != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Titile) && i.Titile.ToLower().Contains(keyword.ToLower().Trim()));
            }

            if (fromPrice >= 0 && fromPrice != null)
            {
                if (toPrice > 0 && toPrice != null && toPrice >= fromPrice)
                {
                    query = query.Where(i => i.PriceTo != null && i.Price >= fromPrice && i.PriceTo >= toPrice || i.PriceTo == null && i.Price >= fromPrice && i.Price <= toPrice);
                }
                if (toPrice == null)
                {
                    query = query.Where(i => i.Price >= fromPrice && i.Price <= toPrice);
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

            var sQuery = query.Where(i => i.Status == (int)PostStatus.Showing).OrderByDescending(i => i.Order).ThenByDescending(i => i.LastModifiedDate);
            var sQuery1 = sQuery.Skip(PageSize * (Page - 1))
                                .Take(PageSize)
                                .ToList();

            var reslist = sQuery.ToList();
            foreach (var item in sQuery)
            {
                var checkUser = await _databaseContext.Customers.FirstOrDefaultAsync(i => i.Id == Guid.Parse(item.UserId));
                if (checkUser != null)
                {
                    item.avatar = checkUser.Avatar;
                }
            }
            return new PagedList<SearchBoughtPostDTO>
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
                check.Status = rq.Status;
                check.Price = rq.Price;
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
                value = value.Where(i => i.Region.ToLower().Contains(region.ToLower())).ToList();
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
        private decimal Price(Guid Id)
        {
            var priceconfig = _databaseContext.PriceConfigurations.FirstOrDefault(i => i.Id == Id);
            if (priceconfig == null) throw new Exception("Không có gói giá này");
            else
            {
                var price = priceconfig.Price;
                var date = priceconfig.Date;
                var priceUser = price * date;
                return priceUser;
            }

        }
        public async Task<int> AddSalePost(SalePost rq, bool? isEnoughWallet, bool? isEnoughWalletPro, double numofDate, Guid GroupPriceId, CancellationToken cancellationToken)
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
            rq.Order = DateTime.Now;
            rq.LastModifiedDate = DateTime.Now;
            rq.CreatedDate = DateTime.Now;
            await _context.SalePosts.AddAsync(rq);
            var res = await _context.SaveChangesAsync(cancellationToken);
            // Kiểm tra ví 
            var user = await _wcontext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == rq.UserId);
            var AmountWallets = user.Amount;
            var userpromotion = await _wcontext.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId.ToString() == rq.UserId);
            var AmountPromotion = userpromotion.Amount;
            //_____________________
            var PriceConfig = Price(GroupPriceId);
            if (AmountPromotion > 0 && AmountPromotion >= PriceConfig)
            {
                await SubtractMoneyPromotional(rq.Id, PriceConfig, cancellationToken);
                await SaveHistory($"{rq.Titile}", AmountWallets, AmountPromotion - PriceConfig, (double)PriceConfig, 1, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {PriceConfig}đ", cancellationToken);
                string body = "";
                if (rq.Type == (int)PostType.Normal)
                {
                    body = "Tin Thường";
                }
                if (rq.Type == (int)PostType.Golden)
                {
                    body = "Tin Vip Đặc Biệt";
                }
                if (rq.Type == (int)PostType.Vip)
                {
                    body = "Tin Vip";
                }
                var rqNotifi = new Notification();
                rqNotifi.Content = $"Trừ tiền đăng tin -{PriceConfig} VND";
                rqNotifi.UserId = rq.UserId;
                await CreateNotification(rqNotifi, cancellationToken);
            }
            else if (AmountPromotion >= 0 && AmountPromotion < PriceConfig)
            {
                var Deduct = PriceConfig - AmountPromotion;
                await SubtractMoneyPromotional(rq.Id, AmountPromotion, cancellationToken);
                await SaveHistory($"{rq.Titile}", AmountWallets, AmountPromotion - Deduct,
                    (double)AmountPromotion, 1, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {AmountPromotion}đ", cancellationToken);
                await SubtractMoney(rq.Id, Deduct, cancellationToken);
                await SaveHistory($"{rq.Titile}", AmountWallets - Deduct, AmountPromotion,
                    (double)Deduct, 0, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {Deduct}đ", cancellationToken);
                string body = "";
                if (rq.Type == (int)PostType.Normal)
                {
                    body = "Tin Thường";
                }
                if (rq.Type == (int)PostType.Golden)
                {
                    body = "Tin Vip Đặc Biệt";
                }
                if (rq.Type == (int)PostType.Vip)
                {
                    body = "Tin Vip";
                }
                var rqNotifi = new Notification();
                rqNotifi.Content = $"Trừ tiền đăng tin -{AmountPromotion} vào tài khoản khuyến mại và -{Deduct} vào tài khoản chính VND";
                rqNotifi.UserId = rq.UserId;
                await CreateNotification(rqNotifi, cancellationToken);
            }


            //if (rq.Type == (int)PostType.Normal)
            //{

            //    if (AmountPromotion > 0 && AmountPromotion >= (decimal)(_configuration.GetValue<int>("Price:Normal") * numofDate))
            //    {
            //        var Fee = (decimal)(_configuration.GetValue<int>("Price:Normal") * numofDate);
            //        await SubtractMoneyPromotional(rq.Id, Fee, cancellationToken);
            //        await SaveHistory($"{rq.Titile}", AmountWallets, AmountPromotion - Fee, _configuration.GetValue<int>("Price:Normal") * numofDate, 1, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {Fee}đ", cancellationToken);
            //    }
            //    else if (AmountPromotion >= 0 && AmountPromotion < (decimal)(_configuration.GetValue<int>("Price:Normal") * numofDate))
            //    {
            //        var Deduct = (decimal)(_configuration.GetValue<int>("Price:Normal") * numofDate) - AmountPromotion;
            //        await SubtractMoneyPromotional(rq.Id, AmountPromotion, cancellationToken);
            //        await SaveHistory($"{rq.Titile}", AmountWallets, AmountPromotion - Deduct, (double)AmountPromotion, 1, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {Deduct}đ", cancellationToken);
            //        await SubtractMoney(rq.Id, Deduct, cancellationToken);
            //        await SaveHistory($"{rq.Titile}", AmountWallets - Deduct, AmountPromotion, (double)Deduct, 0, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {Deduct}đ", cancellationToken);
            //    }
            //}
            //else if (rq.Type == (int)PostType.Golden)
            //{
            //    if (AmountPromotion > 0 && AmountPromotion >= (decimal)(_configuration.GetValue<int>("Price:Vip") * numofDate))
            //    {
            //        var Fee = (decimal)(_configuration.GetValue<int>("Price:Vip") * numofDate);
            //        await SubtractMoneyPromotional(rq.Id, (decimal)(_configuration.GetValue<int>("Price:Vip") * numofDate), cancellationToken);
            //        await SaveHistory($"{rq.Titile}", AmountWallets, AmountPromotion - Fee, _configuration.GetValue<int>("Price:Vip") * numofDate, 1, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {Fee}đ", cancellationToken);
            //    }
            //    else if (AmountPromotion >= 0 && AmountPromotion < (decimal)(_configuration.GetValue<int>("Price:Vip") * numofDate))
            //    {
            //        var Deduct = (decimal)(_configuration.GetValue<int>("Price:Vip") * numofDate) - AmountPromotion;
            //        await SubtractMoneyPromotional(rq.Id, AmountPromotion, cancellationToken);
            //        await SaveHistory($"{rq.Titile}", AmountWallets, AmountPromotion - Deduct, (double)AmountPromotion, 1, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {Deduct}đ", cancellationToken);
            //        await SubtractMoney(rq.Id, Deduct, cancellationToken);
            //        await SaveHistory($"{rq.Titile}", AmountWallets - Deduct, AmountPromotion, (double)Deduct, 0, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {Deduct}đ", cancellationToken);
            //    }
            //}
            //else if (rq.Type == (int)PostType.Vip)
            //{
            //    if (AmountPromotion > 0 && AmountPromotion >= (decimal)(_configuration.GetValue<int>("Price:SuperVip") * numofDate))
            //    {
            //        var Fee = (decimal)(_configuration.GetValue<int>("Price:Normal") * numofDate);
            //        await SubtractMoneyPromotional(rq.Id, (decimal)(_configuration.GetValue<int>("Price:SuperVip") * numofDate), cancellationToken);
            //        await SaveHistory($"{rq.Titile}", AmountWallets, AmountPromotion - Fee, _configuration.GetValue<int>("Price:SuperVip") * numofDate, 1, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {Fee}đ", cancellationToken);
            //    }
            //    else if (AmountPromotion >= 0 && AmountPromotion < (decimal)(_configuration.GetValue<int>("Price:SuperVip") * numofDate))
            //    {
            //        var Deduct = (decimal)(_configuration.GetValue<int>("Price:SuperVip") * numofDate) - AmountPromotion;
            //        await SubtractMoneyPromotional(rq.Id, AmountPromotion, cancellationToken);
            //        await SaveHistory($"{rq.Titile}", AmountWallets, AmountPromotion - Deduct, (double)AmountPromotion, 1, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {Deduct}đ", cancellationToken);
            //        await SubtractMoney(rq.Id, Deduct, cancellationToken);
            //        await SaveHistory($"{rq.Titile}", AmountWallets - Deduct, AmountPromotion, (double)Deduct, 0, Guid.Parse(rq.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {Deduct}đ", cancellationToken);
            //    }
            //}

            return res;
        }
        public async Task<int> RepostSalePost(string? postId, int type, double numberofDate, CancellationToken cancellationToken)
        {
            var post = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == postId);
            if (post == null) throw new ArgumentException("No post found !!!");


            var user = await _wcontext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == post.UserId);
            var AmountWallets = user.Amount;
            var userpromotion = await _wcontext.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId.ToString() == post.UserId);
            var AmountPromotion = userpromotion.Amount;
            var check = await CheckBalancePromotional(post.UserId, post.Type, numberofDate);// tru tien vi km
            var check1 = await CheckBalance(post.UserId, post.Type, numberofDate); // tru tien vi chinh
            if (!check && !check1) throw new ArgumentException("Not enough money");
            if (type == (int)PostType.Normal)
            {
                if (check)
                {
                    var Fee = (decimal)(_configuration.GetValue<int>("Price:Normal") * numberofDate);
                    await SubtractMoneyPromotional(postId, Fee, cancellationToken);
                    await SaveHistory($"{post.Titile}", AmountWallets, AmountPromotion - Fee, _configuration.GetValue<int>("Price:Normal") * numberofDate, 1, Guid.Parse(post.UserId), 1, $"Khách hàng đăng lại tin bán , Giá tin: {Fee}đ", cancellationToken);
                    string body = "";
                    if (post.Type == (int)PostType.Normal)
                    {
                        body = "Tin Thường";
                    }
                    if (post.Type == (int)PostType.Golden)
                    {
                        body = "Tin Vip";
                    }
                    if (post.Type == (int)PostType.Vip)
                    {
                        body = "Tin Vip đặc biệt";
                    }
                    var rqNotifi = new Notification();
                    rqNotifi.Content = $"Trừ tiền đăng tin {body}-{Fee} VND";
                    rqNotifi.UserId = post.UserId;
                    await CreateNotification(rqNotifi, cancellationToken);
                }
                else if (!check && check1)
                {
                    var Fee = (decimal)(_configuration.GetValue<int>("Price:Normal") * numberofDate);
                    await SubtractMoney(postId, Fee, cancellationToken);
                    await SaveHistory($"{post.Titile}", AmountWallets - Fee, AmountPromotion, _configuration.GetValue<int>("Price:Normal") * numberofDate, 0, Guid.Parse(post.UserId), 1, $"Khách hàng đăng lại tin bán , Giá tin: {Fee}đ", cancellationToken);
                    string body = "";
                    if (post.Type == (int)PostType.Normal)
                    {
                        body = "Tin Thường";
                    }
                    if (post.Type == (int)PostType.Golden)
                    {
                        body = "Tin Vip";
                    }
                    if (post.Type == (int)PostType.Vip)
                    {
                        body = "Tin Vip đặc biệt";
                    }
                    var rqNotifi = new Notification();
                    rqNotifi.Content = $"Trừ tiền đăng tin {body}-{Fee} VND";
                    rqNotifi.UserId = post.UserId;
                    await CreateNotification(rqNotifi, cancellationToken);
                }
                post.Status = (int)PostStatus.UnApproved;
            }
            else if (type == (int)PostType.Golden)
            {
                if (check)
                {
                    var Fee = (decimal)(_configuration.GetValue<int>("Price:Vip") * numberofDate);
                    await SubtractMoneyPromotional(postId, Fee, cancellationToken);
                    await SaveHistory($"{post.Titile}", AmountWallets, AmountPromotion - Fee, _configuration.GetValue<int>("Price:Vip") * numberofDate, 1, Guid.Parse(post.UserId), 1, $"Khách hàng đăng lại tin bán , Giá tin: {Fee}đ", cancellationToken);
                    string body = "";
                    if (post.Type == (int)PostType.Normal)
                    {
                        body = "Tin Thường";
                    }
                    if (post.Type == (int)PostType.Golden)
                    {
                        body = "Tin Vip";
                    }
                    if (post.Type == (int)PostType.Vip)
                    {
                        body = "Tin Vip đặc biệt";
                    }
                    var rqNotifi = new Notification();
                    rqNotifi.Content = $"Trừ tiền đăng tin {body}-{Fee} VND";
                    rqNotifi.UserId = post.UserId;
                    await CreateNotification(rqNotifi, cancellationToken);
                }
                else if (!check && check1)
                {
                    var Fee = (decimal)(_configuration.GetValue<int>("Price:Vip") * numberofDate);
                    await SubtractMoney(postId, Fee, cancellationToken);
                    await SaveHistory($"{post.Titile}", AmountWallets - Fee, AmountPromotion, _configuration.GetValue<int>("Price:Vip") * numberofDate, 0, Guid.Parse(post.UserId), 1, $"Khách hàng đăng lại tin bán , Giá tin: {Fee}đ", cancellationToken);
                    string body = "";
                    if (post.Type == (int)PostType.Normal)
                    {
                        body = "Tin Thường";
                    }
                    if (post.Type == (int)PostType.Golden)
                    {
                        body = "Tin Vip";
                    }
                    if (post.Type == (int)PostType.Vip)
                    {
                        body = "Tin Vip đặc biệt";
                    }
                    var rqNotifi = new Notification();
                    rqNotifi.Content = $"Trừ tiền đăng tin {body}-{Fee} VND";
                    rqNotifi.UserId = post.UserId;
                    await CreateNotification(rqNotifi, cancellationToken);
                }
                post.Status = (int)PostStatus.Showing;
            }
            else if (type == (int)PostType.Vip)
            {
                if (check)
                {
                    var Fee = (decimal)(_configuration.GetValue<int>("Price:SuperVip") * numberofDate);
                    await SubtractMoneyPromotional(postId, Fee, cancellationToken);
                    await SaveHistory($"{post.Titile}", AmountWallets, AmountPromotion - Fee, _configuration.GetValue<int>("Price:SuperVip") * numberofDate, 1, Guid.Parse(post.UserId), 1, $"Khách hàng đăng lại tin bán , Giá tin: {Fee}đ", cancellationToken);
                    string body = "";
                    if (post.Type == (int)PostType.Normal)
                    {
                        body = "Tin Thường";
                    }
                    if (post.Type == (int)PostType.Golden)
                    {
                        body = "Tin Vip";
                    }
                    if (post.Type == (int)PostType.Vip)
                    {
                        body = "Tin Vip đặc biệt";
                    }
                    var rqNotifi = new Notification();
                    rqNotifi.Content = $"Trừ tiền đăng tin {body}-{Fee} VND";
                    rqNotifi.UserId = post.UserId;
                    await CreateNotification(rqNotifi, cancellationToken);
                }
                else if (!check && check1)
                {
                    var Fee = (decimal)(_configuration.GetValue<int>("Price:SuperVip") * numberofDate);
                    await SubtractMoney(postId, Fee, cancellationToken);
                    await SaveHistory($"{post.Titile}", AmountWallets - Fee, AmountPromotion, _configuration.GetValue<int>("Price:SuperVip") * numberofDate, 0, Guid.Parse(post.UserId), 1, $"Khách hàng đăng lại tin bán , Giá tin: {Fee}đ", cancellationToken);
                    string body = "";
                    if (post.Type == (int)PostType.Normal)
                    {
                        body = "Tin Thường";
                    }
                    if (post.Type == (int)PostType.Golden)
                    {
                        body = "Tin Vip";
                    }
                    if (post.Type == (int)PostType.Vip)
                    {
                        body = "Tin Vip đặc biệt";
                    }
                    var rqNotifi = new Notification();
                    rqNotifi.UserId = post.UserId;
                    rqNotifi.Content = $"Trừ tiền đăng tin {body}-{Fee} VND";
                    await CreateNotification(rqNotifi, cancellationToken);
                }
                post.Status = (int)PostStatus.Showing;
            }
            post.Order = DateTime.Now;
            post.CreatedDate = DateTime.Now;
            post.LastModifiedDate = DateTime.Now;
            post.Type = type;
            post.DueDate = post.DueDate.Value.AddDays(numberofDate);
            var res = await _context.SaveChangesAsync(cancellationToken);
            return res;
        }

        public async Task SaveHistory(string? Title, decimal? Amount, decimal? Walletamount, double? amount, int? walletType, Guid? cusId, int? type, string? moneyType, CancellationToken cancellationToken)
        {
            if (amount != 0)
            {
                var his = new TransactionHistory()
                {
                    Id = Guid.NewGuid(),
                    Type = type,
                    Content = moneyType,
                    TransactionAmount = amount,
                    WalletType = walletType,
                    CustomerId = cusId,
                    CreatedDate = DateTime.Now,
                    Title = Title,
                    Amount = Amount,
                    Walletamount = Walletamount,
                };
                await _wcontext.TransactionHistorys.AddAsync(his);
                his.Title = $"[{his.TransactionCode}] {Title}";
                await _wcontext.SaveChangesAsync(cancellationToken);
            }
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
        public async Task<PagedList<SalePost>> SearchSalePost(string? Id, string? userid, string? title, int? status, int? type,
            DateTime? fromDate, DateTime? toDate, string? sortFeild, bool? sortValue, int Page, int PageSize)
        {
            var query = _context.SalePosts.AsQueryable();
            if (Id != null && Id.Length > 0)
            {
                query = query.Where(i => i.Id.Equals(Id));

            }
            if (title != null && title.Length > 0)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Titile) && i.Titile.ToLower().Contains(title.ToLower().Trim()));
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
                var sQuery = query.Where(i => i.UserId == userid).OrderByDescending(i => i.LastModifiedDate);
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
                var sQuery = query.OrderByDescending(i => i.LastModifiedDate);
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
        public async Task<PagedList<SearchSalePostDTO>> GetShowingSalePost(string? userid, string? keyword, double? fromPrice, double? toPrice, double? fromArea, double? toArea,
            string? region, int Page, int PageSize)
        {
            var result = await _databaseContext.SalePosts.ToListAsync();
            var query = _mapper.Map<List<SearchSalePostDTO>>(result).AsEnumerable();
            foreach (var item in query)
            {
                if (item.Status == 1)
                {
                    item.MaxSale = MaxSalePost(item);
                    item.MinSale = MinSalePost(item);
                }
                else
                {
                    item.MaxSale = "NoMaxSale";
                    item.MinSale = "NoMinSale";
                }
            }
            if (keyword != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Titile) && i.Titile.ToLower().Contains(keyword.ToLower().Trim()));
            }

            if (fromPrice >= 0)
            {
                if (toPrice > 0)
                {
                    query = query.Where(i => i.Price > 0 && (i.Price >= fromPrice && i.Price < toPrice));
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
                .OrderByDescending(i => i.Type).ThenByDescending(i => i.LastModifiedDate);
            foreach (var item in sQuery)
            {
                var checkUser = await _databaseContext.Customers.FirstOrDefaultAsync(i => i.Id == Guid.Parse(item.UserId));
                if (checkUser != null)
                {
                    item.avatar = checkUser.Avatar;
                }
            }
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
        public async Task<int> UpdateSalePostAdmin(string Id, string? Title, string? Description, int? Status, List<string>? Image, CancellationToken cancellationToken)
        {
            var check = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == Id);
            if (check != null)
            {
                check.Titile = Title != null ? Title : check.Titile;
                check.Description = Description != null ? Description : check.Description;
                check.Status = Status != null ? (int)Status : check.Status;
                check.Image = (Image != null && Image.Count != 0) ? Image : check.Image;
                check.ChangeDate = DateTime.Now;
                return await _context.SaveChangesAsync(cancellationToken);
            }
            return 0;
        }
        public async Task<int> UpdateSaleAdminV2(UpdateSalePostAdminV2C rq, CancellationToken cancellationToken)
        {
            var check = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == rq.Id);
            if (check != null)
            {
                var map = _mapper.Map<UpdateSalePostAdminV2C, SalePost>(rq, check);
                map.LastModifiedDate = DateTime.Now;
                return await _context.SaveChangesAsync(cancellationToken);
            }
            return 0;
        }
        public async Task<int> UpdateBoughtPostAdmin(string Id, string? Title, string? Description, int? Status, List<string>? Image, CancellationToken cancellationToken)
        {
            var check = await _context.BoughtPosts.FirstOrDefaultAsync(i => i.Id == Id);
            if (check != null)
            {
                check.Titile = Title != null ? Title : check.Titile;
                check.Description = Description != null ? Description : check.Description;
                check.Status = Status != null ? (int)Status : check.Status;
                check.Image = (Image != null && Image.Count != 0) ? Image : check.Image;
                check.ChangeDate = DateTime.Now;
                return await _context.SaveChangesAsync(cancellationToken);
            }
            return 0;
        }
        public async Task<int> UpdateSalePost(UpdateSalePostCommand rq, double? numberOfDate, CancellationToken cancellationToken)
        {
            var check = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == rq.Id);
            var user = await _wcontext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == check.UserId);
            var AmountWallets = user.Amount;
            var userpromotion = await _wcontext.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId.ToString() == check.UserId);
            var AmountPromotion = userpromotion.Amount;
            if (check == null) throw new ArgumentException("Can not find!");
            else
            {
                _mapper.Map<UpdateSalePostCommand, SalePost>(rq, check);
                // check.Status = (int)rq.Status;
                check.Type = rq.Type;
                check.Price = rq.Price;
                check.LastModifiedDate = DateTime.Now;


                bool checkWP = await CheckBalancePromotional(check.UserId, rq.Type, numberOfDate);// tru tien vi km
                bool checkW = await CheckBalance(check.UserId, rq.Type, numberOfDate); // tru tien vi chinh
                if (!checkW && !checkWP) throw new ArgumentException("Not enough money");
                if (rq.Type == (int)PostType.Normal)
                {
                    if (checkWP)
                    {
                        var Fee = (decimal)(_configuration.GetValue<int>("Price:Normal") * numberOfDate);
                        await SubtractMoneyPromotional(rq.Id, Fee, cancellationToken);
                        await SaveHistory($"{check.Titile}", AmountWallets, AmountPromotion - Fee, _configuration.GetValue<int>("Price:Normal") * numberOfDate, 1, Guid.Parse(check.UserId), 1, $"Khách hàng thay đổi gói cho bài bán, Giá bài bán {Fee}", cancellationToken);
                    }
                    else if (!checkWP && checkW)
                    {
                        var Fee = (decimal)(_configuration.GetValue<int>("Price:Normal") * numberOfDate);
                        await SubtractMoney(rq.Id, Fee, cancellationToken);
                        await SaveHistory($"{check.Titile}", AmountWallets - Fee, AmountPromotion, _configuration.GetValue<int>("Price:Normal") * numberOfDate, 0, Guid.Parse(check.UserId), 1, $"Khách hàng thay đổi gói cho bài bán, Giá bài bán {Fee}", cancellationToken);
                    }
                    check.Status = (int)PostStatus.UnApproved;
                }
                else if (rq.Type == (int)PostType.Golden)
                {
                    if (checkWP)
                    {
                        var Fee = (decimal)(_configuration.GetValue<int>("Price:Vip") * numberOfDate);
                        await SubtractMoneyPromotional(rq.Id, Fee, cancellationToken);
                        await SaveHistory($"{check.Titile}", AmountWallets, AmountPromotion - Fee, _configuration.GetValue<int>("Price:Vip") * numberOfDate, 1, Guid.Parse(check.UserId), 1, $"Khách hàng thay đổi gói cho bài bán, Giá bài bán {Fee}", cancellationToken);
                    }
                    else if (!checkWP && checkW)
                    {
                        var Fee = (decimal)(_configuration.GetValue<int>("Price:Vip") * numberOfDate);
                        await SubtractMoney(rq.Id, Fee, cancellationToken);
                        await SaveHistory($"{check.Titile}", AmountWallets - Fee, AmountPromotion, _configuration.GetValue<int>("Price:Vip") * numberOfDate, 0, Guid.Parse(check.UserId), 1, $"Khách hàng thay đổi gói cho bài bán, Giá bài bán {Fee}", cancellationToken);
                    }
                    check.Status = (int)PostStatus.Showing;
                }
                else if (rq.Type == (int)PostType.Vip)
                {
                    if (checkWP)
                    {
                        var Fee = (decimal)(_configuration.GetValue<int>("Price:SuperVip") * numberOfDate);
                        await SubtractMoneyPromotional(rq.Id, Fee, cancellationToken);
                        await SaveHistory($"{check.Titile}", AmountWallets, AmountPromotion - Fee, _configuration.GetValue<int>("Price:SuperVip") * numberOfDate, 1, Guid.Parse(check.UserId), 1, $"Khách hàng thay đổi gói cho bài bán, Giá bài bán {Fee}", cancellationToken);
                    }
                    else if (!checkWP && checkW)
                    {
                        var Fee = (decimal)(_configuration.GetValue<int>("Price:SuperVip") * numberOfDate);
                        await SubtractMoney(rq.Id, Fee, cancellationToken);
                        await SaveHistory($"{check.Titile}", AmountWallets - Fee, AmountPromotion, _configuration.GetValue<int>("Price:SuperVip") * numberOfDate, 0, Guid.Parse(check.UserId), 1, $"Khách hàng thay đổi gói cho bài bán, Giá bài bán {Fee}", cancellationToken);
                    }
                    check.Status = (int)PostStatus.Showing;
                }

                check.DueDate = check.DueDate.Value.AddDays((double)numberOfDate);

                return await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<DetailSalePost> ViewDetailSalePost(string id, string UserId)
        {
            var res = await _databaseContext.SalePosts.FirstOrDefaultAsync(i => i.Id == id);
            if (res == null) throw new ArgumentException("Can not find!");
            var map = _mapper.Map<DetailSalePost>(res);
            var CusId = Guid.Parse(res.UserId);

            var Cus = await _databaseContext.Customers.FirstOrDefaultAsync(i => i.Id == CusId);
            map.AddressUser = Cus.Address;

            return map;
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
                    item.ApprovalDate = DateTime.Now;
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
                    item.ApprovalDate = DateTime.Now;
                }
                var result = await _context.SaveChangesAsync(cancellationToken);
                foreach (var item2 in res)
                {
                    var user = await _wcontext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == item2.UserId);
                    var AmountWallets = user.Amount;
                    var userpromotion = await _wcontext.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId.ToString() == item2.UserId);
                    var AmountPromotion = userpromotion.Amount;
                    if (status == (int)PostStatus.Rejected)
                    {
                        var dif = (item2.DueDate - item2.CreatedDate).Value.TotalDays;
                        await ReturnMoney(item2.Id, (decimal)(_configuration.GetValue<int>("Price:Normal") * dif), 0, cancellationToken);
                        var Fee = (decimal)(_configuration.GetValue<int>("Price:Normal") * dif);
                        await SaveHistory($"{item2.Titile}", AmountWallets, AmountPromotion + Fee, _configuration.GetValue<int>("Price:Normal") * dif, 0, Guid.Parse(item2.UserId), 2, $"Hoàn tiền khách hàng khi hủy đăng bài đăng, Giá bài đăng {Fee}đ", cancellationToken);
                        var rqNotifi = new Notification();
                        rqNotifi.UserId = item2.UserId;
                        string body = "";
                        if (item2.Type == (int)PostType.Normal)
                        {
                            body = "Tin Thường";
                        }
                        if (item2.Type == (int)PostType.Golden)
                        {
                            body = "Tin Vip";
                        }
                        if (item2.Type == (int)PostType.Vip)
                        {
                            body = "Tin Vip đặc biệt";
                        }
                        rqNotifi.Content = $"Hoàn tiền từ chối tin +{Fee} VND";
                        await CreateNotification(rqNotifi, cancellationToken);
                    }
                }
                return result;
            }
        }
        public async Task<int> CreateNotification(Notification rq, CancellationToken cancellationToken)
        {
            rq.IsSeen = false;
            rq.CreatedDate = DateTime.Now;
            await _databaseContext.Notification.AddAsync(rq);
            return await _databaseContext.SaveChangesAsync(cancellationToken);
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
        public async Task<bool> CheckAmound(string userId, int type, double? num, Guid GroupPriceId)
        {
            var check = false;
            var user = await _wcontext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == userId);
            if (user == null) throw new ArgumentException("User Not Found !!!");

            var userpromotion = await _wcontext.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId.ToString() == userId);

            if (userpromotion == null) throw new ArgumentException("User Not Found !!!");
            var CheckAmout = user.Amount + userpromotion.Amount;
            //int normalPrice = 0;
            //int vipPrice = 0;
            //int superVipPrice = 0;
            int checkPrice = 0;
            var priceConfig = Price(GroupPriceId);



            if (type == (int)PostType.Normal && CheckAmout > 0 && CheckAmout > priceConfig)
            {
                check = true;
            }
            else if (type == (int)PostType.Golden && CheckAmout > 0 && CheckAmout > priceConfig)
            {
                check = true;
            }
            else if (type == (int)PostType.Vip && CheckAmout > 0 && CheckAmout > priceConfig)
            {
                check = true;
            }
            return check;
        }
        public async Task<bool> CheckBalance(string userId, int type, double? num)
        {
            var check = false;
            var user = await _wcontext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == userId);
            if (user == null) throw new ArgumentException("User Not Found !!!");

            int normalPrice = 0;
            int vipPrice = 0;
            int superVipPrice = 0;

            if (num > 0)
            {
                normalPrice = (int)(_configuration.GetValue<int>("Price:Normal") * num);
                vipPrice = (int)(_configuration.GetValue<int>("Price:Vip") * num);
                superVipPrice = (int)(_configuration.GetValue<int>("Price:SuperVip") * num);
            }
            else
            {
                normalPrice = _configuration.GetValue<int>("Price:Normal");
                vipPrice = _configuration.GetValue<int>("Price:Vip");
                superVipPrice = _configuration.GetValue<int>("Price:SuperVip");
            }


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
        public async Task<bool> CheckBalancePromotional(string userId, int type, double? num)
        {
            var check = false;
            var user = await _wcontext.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId.ToString() == userId);
            if (user == null) throw new ArgumentException("User Not Found !!!");

            int normalPrice = 0;
            int vipPrice = 0;
            int superVipPrice = 0;

            if (num > 0)
            {
                normalPrice = (int)(_configuration.GetValue<int>("Price:Normal") * num);
                vipPrice = (int)(_configuration.GetValue<int>("Price:Vip") * num);
                superVipPrice = (int)(_configuration.GetValue<int>("Price:SuperVip") * num);
            }
            else
            {
                normalPrice = _configuration.GetValue<int>("Price:Normal");
                vipPrice = _configuration.GetValue<int>("Price:Vip");
                superVipPrice = _configuration.GetValue<int>("Price:SuperVip");
            }

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
                //var regionsBought = await _context.BoughtPosts.Where(i => i.Status == (int)PostStatus.Showing)
                //.GroupBy(p => p.Region)
                //.Select(g => new PostDto { Region = g.Key ?? "Unknown", Count = g.Count() })
                //.ToListAsync();
                //return regionsBought;
                var value = new List<PostDto>();
                var checkRegion = await _context.Wards.OrderBy(i => i.Order).ToListAsync();
                foreach (var item in checkRegion)
                {
                    var checkbought = await _context.BoughtPosts.Where(i => i.Region.Equals(item.Name) && i.Status == (int)PostStatus.Showing).ToListAsync();
                    var regionbought = new PostDto()
                    {
                        Region = item.Name ?? "Unknown",
                        Count = checkbought.Count()
                    };
                    if (regionbought.Count > 0)
                    {
                        value.Add(regionbought);
                    }
                }
                return value;
            }
            else
            {
                //var regionsSale = await _context.SalePosts.Where(i => i.Status == (int)PostStatus.Showing)
                //.GroupBy(p => p.Region)
                //.Select(g => new PostDto { Region = g.Key ?? "Unknown", Count = g.Count() })
                //.ToListAsync();
                //return regionsSale;
                var value = new List<PostDto>();
                var checkRegion = await _context.Wards.OrderBy(i => i.Order).ToListAsync();
                foreach (var item in checkRegion)
                {
                    var checksale = await _context.SalePosts.Where(i => i.Region.Equals(item.Name) && i.Status == (int)PostStatus.Showing).ToListAsync();
                    var regionsale = new PostDto()
                    {
                        Region = item.Name ?? "Unknown",
                        Count = checksale.Count()
                    };
                    if (regionsale.Count > 0)
                    {
                        value.Add(regionsale);
                    }
                }
                return value;
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
        #region NewPost
        public async Task<int> AddNewPost(NewPost rq, CancellationToken cancellationToken)
        {
            await _context.NewPosts.AddAsync(rq);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateNewPost(NewPost rq, CancellationToken cancellationToken)
        {
            var check = await _context.NewPosts.FirstOrDefaultAsync(i => i.Id == rq.Id);
            if (check == null) throw new ArgumentException("Can not find");
            else
            {
                _mapper.Map(rq, check);
                //check = _mapper.Map<NewPost,NewPost>(rq,check);
                return await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<int> DeleteNewPost(List<string> Id, CancellationToken cancellationToken)
        {
            var check = await _context.NewPosts.Where(i => Id.Contains(i.Id)).ToListAsync();
            foreach (var item in check)
            {
                _context.NewPosts.Remove(item);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<NewPost>> GetRandomNewPost(int randomCount)
        {
            var value = await _context.NewPosts.ToListAsync();

            var random = new Random();
            int n = value.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                NewPost temp = value[i];
                value[i] = value[j];
                value[j] = temp;
            }

            return value.Take(randomCount).ToList();
        }
        public async Task<NewPost> ViewDetailNewPost(string id)
        {
            var res = await _context.NewPosts.FirstOrDefaultAsync(i => i.Id == id);
            if (res == null) throw new ArgumentException("Can not find!");
            return res;
        }

        public async Task<PagedList<NewPost>> GetShowingNewPost(string? title, int Page, int PageSize)
        {
            var query = _context.NewPosts.AsQueryable();
            if (title != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Title) && i.Title.ToLower().Contains(title.ToLower().Trim()));
            }
            var sQuery = query.OrderByDescending(i => i.CreatedDate);
            var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                          .Take(PageSize)
                          .ToListAsync();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<NewPost>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
            };
        }

        public async Task<PagedList<NewPost>> SearchNewPost(string? title, int Page, int PageSize)
        {

            //var query = _context.NewPosts.AsQueryable();
            //if (title != null)
            //{
            //    query = query.Where(i => !string.IsNullOrEmpty(i.Title) && i.Title.ToLower().Contains(title.ToLower().Trim()));
            //}
            //if (userid != null)
            //{
            //    var sQuery = query.Where(i => i.UserId == userid).OrderByDescending(i => i.CreatedDate);
            //    var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
            //                   .Take(PageSize)
            //                   .ToListAsync();
            //    var reslist = await sQuery.ToListAsync();
            //    return new PagedList<NewPost>
            //    {
            //        Data = sQuery1,
            //        TotalCount = reslist.Count,
            //    };
            //}
            //else
            //{
            //    var sQuery = query.OrderByDescending(i => i.CreatedDate);
            //    var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
            //                        .Take(PageSize)
            //                        .ToListAsync();

            //}
            var data = _context.NewPosts.AsNoTracking();
            if (!string.IsNullOrEmpty(title))
            {
                data = data.Where(i => i.Title.ToLower().Trim().Contains(title.ToLower().Trim()));
            }
            var value = data.ToList();
            var reponse = new PagedList<NewPost>();
            reponse.TotalCount = data.Count();
            reponse.Data = value.Skip(PageSize * (Page - 1))
                                     .Take(PageSize);
            return reponse;

        }
        #endregion
        #region District

        public async Task<List<District>> SearchDistrict()
        {
            var list = await _context.Districts.OrderBy(i => i.Order).ToListAsync();
            return list;
        }
        #endregion
        #region Ward
        public async Task<int> AddWard(Ward rq, CancellationToken cancellationToken)
        {
            await _context.Wards.AddAsync(rq);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateWard(UpdateWardCommand rq, CancellationToken cancellationToken)
        {
            var check = await _context.Wards.FirstOrDefaultAsync(i => i.Id == rq.Id);
            if (check == null) throw new ArgumentException("Can not find");
            else
            {
                _mapper.Map(rq, check);
                return await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<int> DeleteWard(List<string> Id, CancellationToken cancellationToken)
        {
            var check = await _context.Wards.Where(i => Id.Contains(i.Id)).ToListAsync();
            foreach (var item in check)
            {
                _context.Wards.Remove(item);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PagedList<Ward>> SearchWard(string? name, int Page, int PageSize)
        {
            var query = _context.Wards.AsQueryable();
            if (name != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.Name) && i.Name.ToLower().Contains(name.ToLower().Trim()));
            }
            var sQuery = query.Include(i => i.District).OrderBy(i => i.Order);
            var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                                .Take(PageSize)
                                .ToListAsync();
            var reslist = await sQuery.ToListAsync();
            return new PagedList<Ward>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
            };


        }
        public async Task<PagedList<Ward>> SearchWardByDistrict(string? districtId, string? name, int Page, int PageSize)
        {
            var query = _context.Wards.AsQueryable();
            if (name != null)
            {
                query = query.Where(i => !string.IsNullOrEmpty(i.District.Name) && i.District.Name.ToLower().Contains(name.ToLower().Trim()));
            }
            if (districtId != null)
            {
                query = query.Where(i => i.DistrictId == districtId);
            }
            var sQuery = query.Include(i => i.District).OrderBy(i => i.Order);
            var sQuery1 = await sQuery.Skip(PageSize * (Page - 1))
                                .Take(PageSize)
                                .ToListAsync();

            var reslist = await sQuery.ToListAsync();
            return new PagedList<Ward>
            {
                Data = sQuery1,
                TotalCount = reslist.Count,
            };
        }
        #endregion

        public string MaxSalePost(SearchSalePostDTO rq)
        {
            var check = _context.SalePosts.Where(i => i.Region == rq.Region && i.Status == (int)PostStatus.Showing).OrderByDescending(i => i.Price)
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
            var check = _context.SalePosts.Where(i => i.Region == rq.Region && i.Status == (int)PostStatus.Showing).OrderBy(i => i.Price)
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

        public async Task<int> ChangeStatus(string postId, int postType, int statusType, DateTime? lastModifiedDate, string? lastModifiedBy, CancellationToken cancellationToken)
        {
            //statusType : 0-Hạ Tin, 1-Đẩy tin, 2-Đăng lại
            if (postType == 0)
            {
                // 3: đã mua
                var post = await _context.BoughtPosts.FirstOrDefaultAsync(i => i.Id == postId);
                if (post == null) throw new ArgumentException("Can not find post");
                switch (statusType)
                {
                    case 0:
                        post.Status = (int)PostStatus.Down;
                        break;
                    case 1:
                        post.Order = DateTime.UtcNow;
                        break;
                    case 2:
                        if (post.Status == (int)PostStatus.Down || post.Status == (int)PostStatus.Rejected)
                        {
                            post.Status = (int)PostStatus.UnApproved;
                        }
                        break;
                    case 3:
                        post.Status = (int)PostStatus.Bought;
                        post.ChangeDate = DateTime.Now;
                        break;
                }
                post.LastModifiedDate = lastModifiedDate;
                post.LastModifiedBy = lastModifiedBy;
            }
            else
            {
                //2 : đã bán
                var salePost = await _context.SalePosts.FirstOrDefaultAsync(i => i.Id == postId);
                if (salePost == null) throw new ArgumentException("Can not find post");
                switch (statusType)
                {
                    case 0:
                        salePost.Status = (int)PostStatus.Down;
                        break;
                    case 1:
                        salePost.Order = DateTime.UtcNow;
                        break;
                    case 2:
                        salePost.Status = (int)PostStatus.Sold;
                        salePost.ChangeDate = DateTime.Now;
                        break;
                }
                salePost.LastModifiedDate = lastModifiedDate;
                salePost.LastModifiedBy = lastModifiedBy;
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Ward> GetDetailWard(string id)
        {
            var check = await _context.Wards.Where(i => i.Id.Trim().Equals(id)).Include(i => i.District).FirstOrDefaultAsync();
            return check;

        }
    }
}
