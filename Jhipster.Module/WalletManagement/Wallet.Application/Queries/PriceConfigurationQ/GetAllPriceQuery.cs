using AutoMapper;
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
    public class GetAllPriceQuery : IRequest<List<ViewDetailPriceDTO>>
    {
    }
    public class GetAllPriceQueryHandler : IRequestHandler<GetAllPriceQuery, List<ViewDetailPriceDTO>>
    {
        private readonly ITypePriceRepository _repo;
        private readonly IMapper _mapper;

        public GetAllPriceQueryHandler(ITypePriceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ViewDetailPriceDTO>> Handle(GetAllPriceQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllPri();
        }
    }
}
