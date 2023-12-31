﻿using Jhipster.Crosscutting.Utilities;
using MediatR;
using Post.Application.Commands.SalePostC;
using Post.Application.Commands.WardC;
using Post.Application.DTO;
using Post.Application.DTO.NewPostDTO;
using Post.Application.DTO.SalePostDtos;
using Post.Domain.Entities;

namespace Post.Application.Contracts
{
    public interface IPostRepository
    {
        #region BoughtPost
        Task<int> AddBoughtPost(BoughtPost rq, CancellationToken cancellationToken);
        Task<int> UpdateBoughtPost(BoughtPost rq, CancellationToken cancellationToken);
        Task<int> DeleteBoughtPost(List<string> Id, CancellationToken cancellationToken);
        Task<PagedList<BoughtPost>> SearchBoughtPost(string? Id, string? userid, string? title, string? createBy, string? ward, string? region, int? status, DateTime? createDate, DateTime? fromDate, DateTime? toDate, int Page, int PageSize);
        Task<PagedList<SearchBoughtPostDTO>> GetShowingBoughtPost(string? userid, string? keyword, double? fromPrice, double? toPrice,
            string? region, int Page, int PageSize);
        Task<BoughtDetail> ViewDetailBoughtPost(string id);
        Task<List<BoughtPost>> GetRandomBoughtPost(int randomCount, string? region);

        #endregion


        #region SalePost
        Task<int> AddSalePost(SalePost rq, bool? isEnoughWallet, bool? isEnoughWalletPro, double numofDate, Guid GroupPriceId, CancellationToken cancellationToken);
        Task<int> UpdateSalePost(UpdateSalePostCommand rq, double? numberOfDate, Guid GroupPriceId, CancellationToken cancellationToken);
        Task<int> RepostSalePost(string? postId, int type, double numberofDate, Guid GroupPriceId, bool? IsRepost, CancellationToken cancellationToken);
        Task<int> DeleteSalePost(List<string> Id, CancellationToken cancellationToken);
        Task<PagedList<SalePost>> SearchSalePost(string? Id, string? userid, string? title, string? createBy, string? ward,string? region, int? status, int? type,DateTime? dueDate, DateTime? createDate, DateTime? fromDate, DateTime? toDate, string? sortFeild, bool? sortValue, int Page, int PageSize);
        Task<PagedList<SearchSalePostDTO>> GetShowingSalePost(string? userid, string? keyword, double? fromPrice, double? toPrice, double? fromArea, double? toArea,
            string? region, int Page, int PageSize);
        Task<DetailSalePost> ViewDetailSalePost(string id, string UserId);
        Task<int> UpdateSalePostAdmin(string Id, string? Title, string? Description, int? Status, List<string>? Image, CancellationToken cancellationToken);
        Task<int> UpdateSaleAdminV2(UpdateSalePostAdminV2C rq, CancellationToken cancellationToken);
        Task<int> UpdateBoughtPostAdmin(string Id, string? Title, string? Description, int? Status, List<string>? Image, CancellationToken cancellationToken);

        Task<List<SalePost>> GetRandomSalePost(int Random, string? Region);

        #endregion

        #region NewPost
        Task<int> AddNewPost(NewPost rq, CancellationToken cancellationToken);
        Task<int> UpdateNewPost(NewPost rq, CancellationToken cancellationToken);
        Task<int> DeleteNewPost(List<string> Id, CancellationToken cancellationToken);
        Task<NewPost> ViewDetailNewPost(string id);
        Task<List<NewPoDTO>> GetRandomNewPost(int randomCount);
        Task<PagedList<NewPoDTO>> SearchNewPost(string? title, int Page, int PageSize);
        Task<PagedList<NewPoDTO>> GetShowingNewPost(string? title, int Page, int PageSize);
        #endregion
        #region District
        Task<List<District>> SearchDistrict();
        #endregion
        #region Ward
        Task<int> AddWard(Ward rq, CancellationToken cancellationToken);
        Task<int> UpdateWard(UpdateWardCommand rq, CancellationToken cancellationToken);
        Task<Ward> GetDetailWard(string id);
        Task<int> DeleteWard(List<string> Id, CancellationToken cancellationToken);
        //Task<PagedList<Ward>> GetShowingWard(string? districtId, string? name, int Page, int PageSize);
        Task<PagedList<Ward>> SearchWard(string? name, int Page, int PageSize);
        Task<PagedList<Ward>> SearchWardByDistrict(string? districtId, string? name, int Page, int PageSize);
        #endregion

        #region Admin
        Task<int> ApprovePost(int postType, List<string> id, int status, string? reason, DateTime? modifiedDate, string? modifiedBy, CancellationToken cancellationToken);
        #endregion

        #region Other
        Task<bool> CheckAmound(string userId, int type, double? num, Guid GroupPriceId);
        Task<bool> CheckBalance(string userId, int type, double? num);
        Task<bool> CheckBalancePromotional(string userId, int type, double? num);
        Task SubtractMoney(string? postid, decimal amount, CancellationToken cancellationToken);
        Task SubtractMoneyPromotional(string? postid, decimal amount, CancellationToken cancellationToken);
        Task ReturnMoney(string? postid, decimal amount, int type, CancellationToken cancellationToken);
        Task<bool> CheckTitle(string title, string userid);
        Task<List<PostDto>> GetAllRegion(int? type);
        Task<List<StatusDto>> GetAllStatus(int? type, string userId);
        Task<int> ChangeStatus(string postId, int postType, int statusType, DateTime? lastModifiedDate, string? lastModifiedBy, CancellationToken cancellationToken);
        //Task SaveHistory(double? amount, int? walletType, Guid? cusId, int? type, string? moneyType, CancellationToken cancellationToken);

        #endregion
    }
}
