using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Commands.TermConditionConfigurationC;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.TermConditionConfigurationQ
{
    public class GetTermDetailQuery : IRequest<Domain.Entities.TypeTerm>
    {
        public Guid? Id { get; set; }
    }
    public class GetTermDetailQueryHandler : IRequestHandler<GetTermDetailQuery, Domain.Entities.TypeTerm>
    {
        private readonly ITypeTermRepository _repo;
        private readonly IMapper _mapper;

        public GetTermDetailQueryHandler(ITypeTermRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Domain.Entities.TypeTerm> Handle(GetTermDetailQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetById(request.Id);
        }
    }
}
