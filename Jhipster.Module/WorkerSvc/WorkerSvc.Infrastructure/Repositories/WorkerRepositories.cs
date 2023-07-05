using AutoMapper;
using Jhipster.Infrastructure.Data;
using JHipsterNet.Core.Pagination;
using Microsoft.EntityFrameworkCore;
using Post.Domain.Entities;
using Post.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Domain.Entities;
using WorkerSvc.Application.Persistences;

namespace WorkerSvc.Infrastructure.Repositories
{
    public class WorkerRepositories : IWorkerRepositories
    {
        private readonly ApplicationDatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        public WorkerRepositories(ApplicationDatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }
        public async Task<int> UpdateStatus(string Id, int Status)
        {
            var check = await _databaseContext.SalePosts.FirstOrDefaultAsync(i => i.Id == Id);
            check.Status = Status;
            return await _databaseContext.SaveChangesAsync();
        }

        public async Task<int> UpdateOrderFakeNew(Guid Id)
        {
            var check = await _databaseContext.FakeNew.FirstOrDefaultAsync(i => i.Id == Id);
            check.Order = 0;
            check.OrderMax = 0;
            check.CreatedDate = DateTime.Now;
            return await _databaseContext.SaveChangesAsync();
        }
        #region RepostSale
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
        public async Task<int> RepostSalePost(string? postId, int type, CancellationToken cancellationToken)
        {
            var post = await _databaseContext.SalePosts.FirstOrDefaultAsync(i => i.Id == postId);
            var user = await _databaseContext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == post.UserId);
            var AmountWallets = user.Amount;
            var userpromotion = await _databaseContext.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId.ToString() == post.UserId);
            var AmountPromotion = userpromotion.Amount;
           
            var PriceConfig = Price((Guid)post.PriceId);
            if (PriceConfig >= AmountPromotion + AmountWallets)
            {
                var rqNotifi = new Notification();
               
                rqNotifi.Content = $"Bài đăng lại không thành công do hiện tại không đủ tiền để thực hiện ";
                rqNotifi.UserId = post.UserId;
                await CreateNotification(rqNotifi, cancellationToken);
                return -1;
            }
            else
            {
                if (post.Type == (int)PostType.Normal)
                {
                    post.Status = (int)PostStatus.UnApproved;
                }
                else if (post.Type == (int)PostType.Golden)
                {
                    post.Status = (int)PostStatus.Showing;
                }
                else if (post.Type == (int)PostType.Vip)
                {
                    post.Status = (int)PostStatus.Showing;
                }
                if (AmountPromotion > 0 && AmountPromotion >= PriceConfig)
                {
                    await SubtractMoneyPromotional(post.Id, PriceConfig, cancellationToken);
                    await SaveHistory($"{post.Titile}", AmountWallets, AmountPromotion - PriceConfig, (double)PriceConfig, 1, Guid.Parse(post.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {PriceConfig}đ", cancellationToken);
                    string body = "";
                    if (post.Type == (int)PostType.Normal)
                    {
                        body = "Tin Thường";
                    }
                    if (post.Type == (int)PostType.Golden)
                    {
                        body = "Tin Vip Đặc Biệt";
                    }
                    if (post.Type == (int)PostType.Vip)
                    {
                        body = "Tin Vip";
                    }

                    var rqNotifi = new Notification();
                    rqNotifi.Content = $"Trừ tiền đăng tin -{PriceConfig} VND vào tài khoản khuyến mãi";
                    rqNotifi.UserId = post.UserId;
                    await CreateNotification(rqNotifi, cancellationToken);
                }
                else if (AmountPromotion >= 0 && AmountPromotion < PriceConfig)
                {
                    var Deduct = PriceConfig - AmountPromotion;
                    await SubtractMoneyPromotional(post.Id, AmountPromotion, cancellationToken);
                    await SaveHistory($"{post.Titile}", AmountWallets, AmountPromotion - Deduct,
                        (double)AmountPromotion, 1, Guid.Parse(post.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {AmountPromotion}đ", cancellationToken);
                    await SubtractMoney(post.Id, Deduct, cancellationToken);
                    await SaveHistory($"{post.Titile}", AmountWallets - Deduct, AmountPromotion,
                        (double)Deduct, 0, Guid.Parse(post.UserId), 1, $"Khách hàng thêm mới tin bán , Giá tin: {Deduct}đ", cancellationToken);
                    string body = "";
                    if (post.Type == (int)PostType.Normal)
                    {
                        body = "Tin Thường";
                    }
                    if (post.Type == (int)PostType.Golden)
                    {
                        body = "Tin Vip Đặc Biệt";
                    }
                    if (post.Type == (int)PostType.Vip)
                    {
                        body = "Tin Vip";
                    }
                    var rqNotifi = new Notification();
                    var contentNotif = "";
                    if (AmountPromotion == 0)
                    {
                        contentNotif = $"Trừ tiền đăng tin -{Deduct} VND vào tài khoản chính";
                    }
                    else
                    {
                        contentNotif = $"Trừ tiền đăng tin -{AmountPromotion} VND vào tài khoản khuyến mại và -{Deduct} VND vào tài khoản chính";
                    }
                    rqNotifi.Content = contentNotif;
                    rqNotifi.UserId = post.UserId;
                    await CreateNotification(rqNotifi, cancellationToken);
                }
                var Date = DateTime.Now;
                post.Order = DateTime.Now;
                post.CreatedDate = DateTime.Now;
                post.LastModifiedDate = DateTime.Now;
                post.Type = type;
                post.DueDate = Date.AddDays(DatePrice(post.PriceId));
                post.PriceId = post.PriceId;
                var res = await _databaseContext.SaveChangesAsync(cancellationToken);
                return res;
            }
        }
        public async Task<int> CreateNotification(Notification rq, CancellationToken cancellationToken)
        {
            rq.IsSeen = false;
            rq.CreatedDate = DateTime.Now;
            await _databaseContext.Notification.AddAsync(rq);
            return await _databaseContext.SaveChangesAsync(cancellationToken);
        }
        public async Task SubtractMoneyPromotional(string? postid, decimal amount, CancellationToken cancellationToken)
        {
            if (postid != null)
            {
                var post = await _databaseContext.SalePosts.FirstOrDefaultAsync(i => i.Id == postid);
                if (post == null) throw new ArgumentException("No post found !!!");
                var user = await _databaseContext.WalletPromotionals.FirstOrDefaultAsync(i => i.CustomerId.ToString() == post.UserId);
                if (user == null) throw new ArgumentException("No user found !!!");
                user.Amount -= amount;
                user.LastModifiedDate = DateTime.UtcNow;
                await _databaseContext.SaveChangesAsync(cancellationToken);
            }
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
                await _databaseContext.TransactionHistorys.AddAsync(his);
                his.Title = $"[{his.TransactionCode}] {Title}";
                await _databaseContext.SaveChangesAsync(cancellationToken);
            }
        }
        public async Task SubtractMoney(string? postid, decimal amount, CancellationToken cancellationToken)
        {
            if (postid != null)
            {
                var post = await _databaseContext.SalePosts.FirstOrDefaultAsync(i => i.Id == postid);
                if (post == null) throw new ArgumentException("No post found !!!");
                var user = await _databaseContext.Wallets.FirstOrDefaultAsync(i => i.CustomerId.ToString() == post.UserId);
                if (user == null) throw new ArgumentException("No user found !!!");
                user.Amount -= amount;
                user.LastModifiedDate = DateTime.UtcNow;
                await _databaseContext.SaveChangesAsync(cancellationToken);
            }
        }
        private int DatePrice(Guid? PriceId)
        {
            var checkdate = _databaseContext.PriceConfigurations.FirstOrDefault(i => i.Id == PriceId);
            return checkdate.Date;
        }
        #endregion
    }
}
