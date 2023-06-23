using System;
using System.Threading.Channels;
using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using Post.Application.Commands.BoughtPostC;
using Post.Application.Commands.NewPostC;
using Post.Application.Commands.SalePostC;
using Post.Application.Commands.WardC;
using Post.Application.DTO;
using Post.Application.DTO.SalePostDtos;
using Post.Application.Queries.WardQ;
using Post.Domain.Entities;

namespace Post.Application.Configurations.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region BoughtPost
            CreateMap<BoughtPost, BoughtPost>().ReverseMap().ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<BoughtPost, UpdateBoughtPostCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<AddBoughtPostCommand, BoughtPost>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<BoughtPost, SearchBoughtPostDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

            #endregion 
            #region SalePost
            CreateMap<SalePost, SalePost>().ReverseMap().ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<SalePost, UpdateSalePostCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<SalePost, AddSalePostCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<SalePost, SearchSalePostDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<SalePost, RepostSalePostCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<SalePost, SalePost>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<SalePost, UpdateSalePostAdminV2C>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<SalePost, DetailSalePost>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            #endregion
            #region NewPost
            CreateMap<NewPost, NewPost>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<NewPost, UpdateNewPostCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<NewPost, AddNewPostCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            #endregion
            #region District
            CreateMap<District, District>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<District, DistrictDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

            #endregion
            #region Ward
            CreateMap<Ward, Ward>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<Ward, WardDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<Ward, UpdateWardCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<Ward, AddWardCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap(typeof(PagedList<Ward>), typeof(PagedList<WardDTO>));
            #endregion
        }
    }
}

