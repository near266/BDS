using System;
using AutoMapper;
using Wallet.Application.Commands.CustomerC;
using Wallet.Application.Commands.WalletsC;
using Wallet.Application.Commands.WalletsPromotionaC;
using Wallet.Application.DTO;
using Wallet.Domain.Entities;

namespace Wallet.Application.Configurations.Mapper
{
	public class AutoMapperProfile : Profile
    {
		public AutoMapperProfile()
		{
            //Customer
            CreateMap<Customer, Customer>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<Customer, AddCustomerCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<Customer, UpdateCustomerCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

            //Wallet
            /*CreateMap<WalletEntity, WalletEntity>()
                .ForMember(dest => dest.Amount, opt => opt.Ignore())
                .ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));*/
            CreateMap<WalletEntity, AddWalletsCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<WalletEntity, UpdateWalletCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<WalletEntity, WalletDto>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

            //WalletPromotional
            CreateMap<WalletPromotional, WalletPromotional>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<WalletPromotional, AddWalletPromotionCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<WalletPromotional, UpdateWalletPromotionCommand>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));
            CreateMap<WalletPromotional, WalletPromotionalDto>().ReverseMap().ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

        }
    }
}

