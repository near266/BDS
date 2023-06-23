using AutoMapper;
using Jhipster.Crosscutting.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;

namespace Wallet.Application.Queries.PriceConfigurationQ
{
    public class GetPriceConfigByTypePriceIdQuery : IRequest<PriceConfigurationDTO>
    {
        public Guid Id { get; set; }

    }
    public class ViewAllPriceConfigurationQHandler : IRequestHandler<GetPriceConfigByTypePriceIdQuery, PriceConfigurationDTO>
    {
        private readonly IPriceConfigurationRepository _repo;
        private readonly IMapper _mapper;

        public ViewAllPriceConfigurationQHandler(IPriceConfigurationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PriceConfigurationDTO> Handle(GetPriceConfigByTypePriceIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _repo.GetPriceConfigurationByTypePriceId(request.Id);
            var map = _mapper.Map<PriceConfigurationDTO>(res);
            return map;
        }
    }
}
