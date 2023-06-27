using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.PriceConfigurationC
{
    public class UpdatePriceConfigurationCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public int? Type { get; set; }
        [JsonIgnore]
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public Guid? TypePriceId { get; set; }
        public int? Date { get; set; }
        public decimal? PriceDefault { get; set; }
        public decimal? Discount { get; set; }
        public int? Unit { get; set; }

        [JsonIgnore]
        public string? LastModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }

    }
    public class UpdatePriceConfigurationCommandHandler : IRequestHandler<UpdatePriceConfigurationCommand, int>
    {
        private readonly IPriceConfigurationRepository _repo;
        private readonly IMapper _mapper;

        public UpdatePriceConfigurationCommandHandler(IPriceConfigurationRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdatePriceConfigurationCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<PriceConfiguration>(request);
            return await _repo.Update(map, cancellationToken);
        }
    }
}
