using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Commands.WalletsC;
using Wallet.Application.Commands.WalletsPromotionaC;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.CustomerC
{
    public class AddCustomerCommand : IRequest<int>
    {
        [JsonIgnore]
        public Guid? Id { get; set; }
        public string CustomerName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Company { get; set; }
        public bool? IsUnique { get; set; }
        public string? Avatar { get; set; }
        //Sàn
        public string? Exchange { get; set; }
        public string? ExchangeDescription { get; set; }
        public DateTime? MaintainFrom { get; set; }
        public DateTime? MaintainTo { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
    public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, int>
    {
        private readonly ICustomerRepository _repo;
        private readonly IWalletRepository _wRepo;
        private readonly IWalletPromotionalRepository _wpRepo;
        private readonly IMapper _mapper;
        public AddCustomerCommandHandler(ICustomerRepository repo,IWalletRepository wRepo, IWalletPromotionalRepository wpRepo, IMapper mapper)
        {
            _repo = repo;
            _wRepo = wRepo;
            _wpRepo = wpRepo;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<Customer>(request);
            var res = await _repo.Add(map,cancellationToken);
            var wallet = new WalletEntity
            {
                Id = Guid.NewGuid(),
                Username = "string",
                Amount = 0,
                Currency = "Đồng",
                CustomerId = (Guid)request.Id,
                CreatedDate = DateTime.UtcNow,
            };

            var walletPro = new WalletPromotional
            {
                Id = Guid.NewGuid(),
                Username = "string",
                Amount = 0,
                Currency = "Đồng",
                CustomerId = (Guid)request.Id,
                CreatedDate = DateTime.UtcNow,
            };
            var res1 = await _wRepo.Add(wallet, cancellationToken);
            var res2 = await _wpRepo.Add(walletPro, cancellationToken);
            return res;
        }
    }
}
