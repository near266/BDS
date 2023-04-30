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
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.WalletsPromotionaC
{
    public class AddWalletPromotionCommand : IRequest<int>
    {
        public Guid? Id { get; set; }
        public string? Username { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        public Guid? CustomerId { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]

        public DateTime CreatedDate { get; set; }
    }

    public class UpdateWalletCommandHandler : IRequestHandler<AddWalletPromotionCommand, int>
    {
        private readonly IWalletPromotionalRepository _repo;
        private readonly IMapper _mapper;
        public UpdateWalletCommandHandler(IWalletPromotionalRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddWalletPromotionCommand rq, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<WalletPromotional>(rq);
            return await _repo.Add(obj,cancellationToken);
        }
    }
}
