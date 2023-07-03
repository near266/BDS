using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.TermConditionConfigurationQ
{
    public class ViewAllTermConditionConfigurationQuery : IRequest<IEnumerable<TermConditionConfiguration>>
    {
    }
    public class ViewAllTermConditionConfigurationQueryHandler : IRequestHandler<ViewAllTermConditionConfigurationQuery, IEnumerable<TermConditionConfiguration>>
    {
        private readonly ITermConditionConfigurationRepository _repo;
        private readonly IMapper _mapper;

        public ViewAllTermConditionConfigurationQueryHandler(ITermConditionConfigurationRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TermConditionConfiguration>> Handle(ViewAllTermConditionConfigurationQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAll();
        }
    }
}

