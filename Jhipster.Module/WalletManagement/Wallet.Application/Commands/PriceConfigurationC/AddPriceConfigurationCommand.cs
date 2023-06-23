using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Commands.TypePriceC;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.PriceConfigurationC
{
    public class AddPriceConfigurationCommand : IRequest<int>
    {
        public int Type { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Guid? TypePriceId { get; set; }
        public int Date { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
    }
    public class AddNewPriceConfigurationCommandHandler : IRequestHandler<AddPriceConfigurationCommand, int>
    {
        private readonly IPriceConfigurationRepository _repository;
        private readonly IMapper _mapper;

        public AddNewPriceConfigurationCommandHandler(IPriceConfigurationRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddPriceConfigurationCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<PriceConfiguration>(request);
            return await _repository.Add(map, cancellationToken);
        }
    }
}
