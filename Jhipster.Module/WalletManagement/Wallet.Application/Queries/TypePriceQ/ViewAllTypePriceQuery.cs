using AutoMapper;
using MediatR;
using Post.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.TypePriceQ
{
    public class ViewAllTypePriceQuery : IRequest<IEnumerable<TypePrice>>
    {
    }
    public class ViewAllTypePriceQueryHandler : IRequestHandler<ViewAllTypePriceQuery, IEnumerable<TypePrice>>
    {
        private readonly ITypePriceRepository _repo;
        private readonly IMapper _mapper;

        public ViewAllTypePriceQueryHandler(ITypePriceRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TypePrice>> Handle(ViewAllTypePriceQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAll();
        }
    }
}
