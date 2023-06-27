using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Application.Queries.PriceConfigurationQ;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.PriceConfigurationQ
{
    public class ViewAllPriceConfigurationQuery : IRequest<IEnumerable<PriceConfiguration>>
    {
    }
    public class ViewAllPriceConfigurationQueryHandler : IRequestHandler<ViewAllPriceConfigurationQuery, IEnumerable<PriceConfiguration>>
    {
        private readonly IPriceConfigurationRepository _repo;
        private readonly IMapper _mapper;

        public ViewAllPriceConfigurationQueryHandler(IPriceConfigurationRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PriceConfiguration>> Handle(ViewAllPriceConfigurationQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAll();
        }
    }
}
