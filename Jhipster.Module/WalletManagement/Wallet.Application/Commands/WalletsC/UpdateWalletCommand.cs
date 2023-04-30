﻿    using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.WalletsC
{
    public class UpdateWalletCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        [JsonIgnore]

        public string? LastModifiedBy { get; set; }
        [JsonIgnore]

        public DateTime? LastModifiedDate { get; set; }
    }
    public class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand, int>
    {
        private readonly IWalletRepository _repo;
        private readonly IMapper _mapper;
        public UpdateWalletCommandHandler(IWalletRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateWalletCommand rq, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<WalletEntity>(rq);
            return await _repo.Update(obj, cancellationToken);
        }
    }
}
