using System;
using AutoMapper;
using Post.Application.Commands.BoughtPostC;
using Post.Application.Commands.SalePostC;
using Post.Application.DTO;
using Post.Domain.Entities;

namespace Post.Application.Configurations.Mapper
{
	public class AutoMapperProfile : Profile
    {
		public AutoMapperProfile()
		{
            #region BoughtPost
            CreateMap<BoughtPost, BoughtPost>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<BoughtPost, UpdateBoughtPostCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
			CreateMap<AddBoughtPostCommand,BoughtPost>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            #endregion 
            #region SalePost
            CreateMap<SalePost, SalePost>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<SalePost, UpdateSalePostCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<SalePost, AddSalePostCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<SalePost, SearchSalePostDTO>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
          //  CreateMap<IQueryable<SalePost>, IQueryable<SearchSalePostDTO>>().ReverseMap();
            #endregion

        }
    }
}

