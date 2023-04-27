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
        Task<PagedList<BoughtPost>> GetBoughtPost( int Page, int PageSize);
        Task<BoughtPost> ViewDetailBoughtPost(string id);
        #endregion
        #region SalePost
        Task<int> AddSalePost(SalePost rq, CancellationToken cancellationToken);
        Task<int> UpdateSalePost(SalePost rq, CancellationToken cancellationToken);
        Task<int> DeleteSalePost(string Id, CancellationToken cancellationToken);
        Task<PagedList<SalePost>> GetSalePost(int Page, int PageSize);
        Task<SalePost> ViewDetailSalePost(string id);
        #endregion

    }
}
