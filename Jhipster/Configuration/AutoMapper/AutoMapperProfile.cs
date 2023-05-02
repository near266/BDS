using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Wallet.Domain.Entities;
using Wallet.Application.Commands.WalletsC;
using Wallet.Application.Commands.WalletsPromotionaC;
using Wallet.Application.Commands.CustomerC;
using Jhipster.Dto;

namespace Jhipster.Configuration.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, Dto.UserDto>()
                .ForMember(userDto => userDto.Roles, opt => opt.MapFrom(user => user.UserRoles.Select(iur => iur.Role.Name).ToHashSet()))
            .ReverseMap()
                .ForPath(user => user.UserRoles, opt => opt.MapFrom(userDto => userDto.Roles.Select(role => new UserRole { Role = new Role { Name = role }, UserId = userDto.Id }).ToHashSet()));
            //.ForAllMembers(x => x.Condition((source, target, sourceValue) => sourceValue != null));

            //Wallet
            CreateMap<WalletEntity, WalletEntity>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AddWalletsCommand, WalletEntity>();
            CreateMap<DeleteWalletCommand, WalletEntity>();
            //WalletPromotional
            CreateMap<WalletPromotional, WalletPromotional>().ReverseMap().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AddWalletPromotionCommand, WalletPromotional>();
            CreateMap<DeleteWalletPromotionalCommand, WalletPromotional>();

            //Customer
            CreateMap<UserDto, AddCustomerCommand>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.FirstName));
            CreateMap<ManagedUserDto, AddCustomerCommand>()
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.FirstName));


        }
    }
}
