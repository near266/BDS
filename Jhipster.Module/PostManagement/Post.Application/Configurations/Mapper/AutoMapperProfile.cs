using System;
using AutoMapper;
using Post.Application.Commands.BoughtPostC;
using Post.Application.Commands.SalePostC;
using Post.Domain.Entities;

namespace Post.Application.Configurations.Mapper
{
	public class AutoMapperProfile : Profile
    {
		public AutoMapperProfile()
		{
            #region BoughtPost
            CreateMap<BoughtPost, BoughtPost>().ReverseMap();
			CreateMap<BoughtPost, UpdateBoughtPostCommand>().ReverseMap();
			CreateMap<BoughtPost, AddBoughtPostCommand>().ReverseMap();
            #endregion 
            #region SalePost
            CreateMap<SalePost, SalePost>().ReverseMap();
            CreateMap<SalePost, UpdateSalePostCommand>().ReverseMap();
            CreateMap<SalePost, AddSalePostCommand>().ReverseMap();
            #endregion

        }
    }
}

