using AutoMapper;
using System.Linq;
using Jhipster.Domain;
using Wallet.Domain.Entities;
using Wallet.Application.Commands.WalletsC;
using Wallet.Application.Commands.WalletsPromotionaC;

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

        }
    }
}
