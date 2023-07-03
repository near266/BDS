using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.TermConditionConfigurationQ
{
    public class ViewDetailTermConditionConfigurationQuery : IRequest<TermConditionConfigurationDTO>
    {
        public Guid Id { get; set; }
    }
    public class ViewDetailTermConditionConfigurationQueryHandler : IRequestHandler<ViewDetailTermConditionConfigurationQuery, TermConditionConfigurationDTO>
    {
        private readonly ITermConditionConfigurationRepository _repo;
        private readonly IMapper _mapper;

        public ViewDetailTermConditionConfigurationQueryHandler(ITermConditionConfigurationRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TermConditionConfigurationDTO> Handle(ViewDetailTermConditionConfigurationQuery request, CancellationToken cancellationToken)
        {
            var res = await _repo.ViewDetail(request.Id);
            var map = _mapper.Map<TermConditionConfigurationDTO>(res);
            return map;
        }
    }
}
