using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.TypeTermQ
{
    public class ViewAllTypeTermQuery : IRequest<IEnumerable<TypeTerm>>
    {
    }
    public class ViewAllTypeTermQueryHandler : IRequestHandler<ViewAllTypeTermQuery, IEnumerable<TypeTerm>>
    {
        private readonly ITypeTermRepository _repo;
        private readonly IMapper _mapper;

        public ViewAllTypeTermQueryHandler(ITypeTermRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }
        public async Task<IEnumerable<TypeTerm>> Handle(ViewAllTypeTermQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAll();
        }
    }
}
