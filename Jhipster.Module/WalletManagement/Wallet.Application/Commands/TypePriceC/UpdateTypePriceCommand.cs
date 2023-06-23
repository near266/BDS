using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.TypePriceC
{
    public class UpdateTypePriceCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
    }
    public class UpdateTypePriceCommandHandler : IRequestHandler<UpdateTypePriceCommand, int>
    {
        private readonly ITypePriceRepository _repo;
        private readonly IMapper _mapper;

        public UpdateTypePriceCommandHandler(ITypePriceRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateTypePriceCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<TypePrice>(request);
            return await _repo.Update(map, cancellationToken);
        }
    }
}
