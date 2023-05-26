using Jhipster.Crosscutting.Utilities;
using Post.Application.DTO;
using Post.Domain.Entities;

namespace Post.Application.Contracts
{
    public interface IPostRepository
    {
        #region BoughtPost
        Task<int> AddBoughtPost(BoughtPost rq, CancellationToken cancellationToken);
        Task<int> UpdateBoughtPost(BoughtPost rq, CancellationToken cancellationToken);
        Task<int> DeleteBoughtPost(string Id, CancellationToken cancellationToken);
        Task<PagedList<BoughtPost>> SearchBoughtPost(string? userid, string? title, int? status, int Page, int PageSize);
        Task<PagedList<BoughtPost>> GetShowingBoughtPost(string? userid,string? keyword, int? fromPrice, int? toPrice,
            string? region, int Page, int PageSize);
        Task<BoughtPost> ViewDetailBoughtPost(string id);
        #endregion

        Task<List<PostDto>> GetAllRegion(int? type);

        #region SalePost
        Task<int> AddSalePost(SalePost rq, bool? isEnoughWallet, bool? isEnoughWalletPro, double numofDate, CancellationToken cancellationToken);
        Task<int> UpdateSalePost(SalePost rq, CancellationToken cancellationToken);
        Task<int> DeleteSalePost(string Id, CancellationToken cancellationToken);
        Task<PagedList<SalePost>> SearchSalePost(string? userid, string? title, int? status, int? type, int Page, int PageSize);
        Task<PagedList<SalePost>> GetShowingSalePost(string? userid,string? keyword, int? fromPrice, int? toPrice, double? fromArea, double? toArea,
            string? region, int Page, int PageSize);
        Task<SalePost> ViewDetailSalePost(string id);
        Task SubtractMoney(string? postid, decimal amount, CancellationToken cancellationToken);
        Task SubtractMoneyPromotional(string? postid, decimal amount, CancellationToken cancellationToken);
        #endregion
        Task<bool> CheckBalance(string userId, int type);
        Task<bool> CheckBalancePromotional(string userId, int type);

        #region Admin
        Task<int> ApprovePost(int postType, List<string> id, int status, string? reason, DateTime? modifiedDate, string? modifiedBy, CancellationToken cancellationToken);
        #endregion
    }
}
