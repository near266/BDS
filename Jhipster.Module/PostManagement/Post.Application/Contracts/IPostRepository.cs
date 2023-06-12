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
        Task<int> DeleteBoughtPost(List<string> Id, CancellationToken cancellationToken);
        Task<PagedList<BoughtPost>> SearchBoughtPost(string? userid, string? title, int? status, DateTime? fromDate, DateTime? toDate, int Page, int PageSize);
        Task<PagedList<BoughtPost>> GetShowingBoughtPost(string? userid, string? keyword, double? fromPrice, double? toPrice,
            string? region, int Page, int PageSize);
        Task<BoughtPost> ViewDetailBoughtPost(string id);
        Task<List<BoughtPost>> GetRandomBoughtPost(int randomCount, string? region);

        #endregion


        #region SalePost
        Task<int> AddSalePost(SalePost rq, bool? isEnoughWallet, bool? isEnoughWalletPro, double numofDate, CancellationToken cancellationToken);
        Task<int> UpdateSalePost(SalePost rq, CancellationToken cancellationToken);
        Task<int> RepostSalePost(string? postId, int type, double numberofDate,CancellationToken cancellationToken);
        Task<int> DeleteSalePost(List<string> Id, CancellationToken cancellationToken);
        Task<PagedList<SalePost>> SearchSalePost(string? userid, string? title, int? status, int? type, DateTime? fromDate, DateTime? toDate, string? sortFeild, bool? sortValue, int Page, int PageSize);
        Task<PagedList<SearchSalePostDTO>> GetShowingSalePost(string? userid, string? keyword, double? fromPrice, double? toPrice, double? fromArea, double? toArea,
            string? region, int Page, int PageSize);
        Task<SalePost> ViewDetailSalePost(string id);
        Task<List<SalePost>> GetRandomSalePost(int Random, string? Region);

        #endregion

        #region NewPost
        Task<int> AddNewPost(NewPost rq, CancellationToken cancellationToken);
        Task<int> UpdateNewPost(NewPost rq, CancellationToken cancellationToken);
        Task<int> DeleteNewPost(List<string> Id, CancellationToken cancellationToken);
        Task<NewPost> ViewDetailNewPost(string id);
        Task<PagedList<NewPost>> SearchNewPost(string? title, int Page, int PageSize);
        Task<PagedList<NewPost>> GetShowingNewPost(string? title, int Page, int PageSize);
        #endregion
        #region District
        Task<List<District>> SearchDistrict();
        #endregion
        #region Ward
        Task<int> AddWard(Ward rq, CancellationToken cancellationToken);
        Task<int> UpdateWard(Ward rq, CancellationToken cancellationToken);
        Task<int> DeleteWard(List<string> Id, CancellationToken cancellationToken);
        //Task<PagedList<Ward>> GetShowingWard(string? districtId, string? name, int Page, int PageSize);
        Task<PagedList<Ward>> SearchWard(string? districtId, string? name, int Page, int PageSize);
        Task<List<Ward>> SearchWardByDistrict(string? districtId,string? name);
        #endregion

        #region Admin
        Task<int> ApprovePost(int postType, List<string> id, int status, string? reason, DateTime? modifiedDate, string? modifiedBy, CancellationToken cancellationToken);
        #endregion

        #region Other
        Task<bool> CheckBalance(string userId, int type);
        Task<bool> CheckBalancePromotional(string userId, int type);
        Task SubtractMoney(string? postid, decimal amount, CancellationToken cancellationToken);
        Task SubtractMoneyPromotional(string? postid, decimal amount, CancellationToken cancellationToken);
        Task ReturnMoney(string? postid, decimal amount, int type, CancellationToken cancellationToken);
        Task<bool> CheckTitle(string title, string userid);
        Task<List<PostDto>> GetAllRegion(int? type);
        Task<List<StatusDto>> GetAllStatus(int? type, string userId);
        Task<int> ChangeStatus(string postId, int postType, int statusType, DateTime? lastModifiedDate, string? lastModifiedBy, CancellationToken cancellationToken);
        Task SaveHistory(double? amount, int? walletType, Guid? cusId, string? type, CancellationToken cancellationToken);

        #endregion
    }
}
