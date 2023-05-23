﻿using AutoMapper;
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

namespace Wallet.Application.Commands.WalletsPromotionaC
{
    public class UpdateWalletPromotionCommand :IRequest<int>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string? Currency { get; set; }
        [JsonIgnore]

        public string? LastModifiedBy { get; set; }
        [JsonIgnore]

        public DateTime? LastModifiedDate { get; set; }
    }
    public class UpdateWalletPromotionCommandHandler : IRequestHandler<UpdateWalletPromotionCommand, int>
    {
        private readonly IWalletPromotionalRepository _repo;
        private readonly IMapper _mapper;
        public UpdateWalletPromotionCommandHandler(IWalletPromotionalRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateWalletPromotionCommand rq, CancellationToken cancellationToken)
        {
            var obj = _mapper.Map<WalletPromotional>(rq);
            return await _repo.Update(obj, cancellationToken);
        }
    }
}