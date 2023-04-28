using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.WalletsC
{
    public  class AddWalletsCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]

        public DateTime CreatedDate { get; set; }
        [JsonIgnore]

        public string? LastModifiedBy { get; set; }
        [JsonIgnore]

        public DateTime? LastModifiedDate { get; set; }
    }
    public class AddWalletsCommandHandler : IRequestHandler<AddWalletsCommand, int> 
    {
        private readonly IWalletRepository _repo;
        private readonly IMapper _mapper;
        public AddWalletsCommandHandler(IWalletRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddWalletsCommand rq , CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<WalletEntity>(rq);
            return await _repo.Add(obj);
        }
    }

}
