using AutoMapper;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Abstractions;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.TypePriceC
{
    public class AddTypePriceCommand : IRequest<int>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public string? CreatedBy { get; set; }
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
    }
    public class AddNewTypePriceCommandHandler : IRequestHandler<AddTypePriceCommand, int>
    {
        private readonly ITypePriceRepository _repo;
        private readonly IMapper _mapper;

        public AddNewTypePriceCommandHandler(ITypePriceRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddTypePriceCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<TypePrice>(request);
            return await _repo.Add(map, cancellationToken);
        }
    }
}
