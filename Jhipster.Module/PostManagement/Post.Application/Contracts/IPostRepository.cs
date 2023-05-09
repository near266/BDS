using Jhipster.Crosscutting.Utilities;
using Post.Domain.Entities;

namespace Post.Application.Contracts
{
    public interface IPostRepository
    {
        #region BoughtPost
        Task<int> AddBoughtPost(BoughtPost rq, CancellationToken cancellationToken);
        Task<int> UpdateBoughtPost(BoughtPost rq, CancellationToken cancellationToken);
        Task<int> DeleteBoughtPost(string Id, CancellationToken cancellationToken);
        Task<PagedList<BoughtPost>> SearchBoughtPost(string? userid, int Page, int PageSize);
        Task<PagedList<BoughtPost>> GetShowingBoughtPost(int Page, int PageSize);
        Task<BoughtPost> ViewDetailBoughtPost(string id);
        #endregion

        #region SalePost
        Task<int> AddSalePost(SalePost rq, CancellationToken cancellationToken);
        Task<int> UpdateSalePost(SalePost rq, CancellationToken cancellationToken);
        Task<int> DeleteSalePost(string Id, CancellationToken cancellationToken);
        Task<PagedList<SalePost>> SearchSalePost(string? userid, int Page, int PageSize);
        Task<PagedList<SalePost>> GetShowingSalePost(int Page, int PageSize);
        Task<SalePost> ViewDetailSalePost(string id);
        Task SubtractMoney (string? postid, decimal amount, CancellationToken cancellationToken);
        #endregion
        Task<bool> CheckBalance(string userId, int type);
        #region Admin
        Task<int> ApprovePost (int postType,string id,int status,string? reason,DateTime? modifiedDate, string? modifiedBy, CancellationToken cancellationToken);
        #endregion
    }
}
