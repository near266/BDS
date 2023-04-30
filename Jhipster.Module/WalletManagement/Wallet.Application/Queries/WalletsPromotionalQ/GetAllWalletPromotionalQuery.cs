using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.WalletsPromotionalQ
{
    public class GetAllWalletPromotionalQuery : IRequest<IEnumerable<WalletPromotional>>
    {
    }
    public class GetAllQueryHandler : IRequestHandler<GetAllWalletPromotionalQuery, IEnumerable<WalletPromotional>>
    {
        private readonly IWalletPromotionalRepository _repo;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IWalletPromotionalRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<WalletPromotional>> Handle(GetAllWalletPromotionalQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAll();
        }
    }
}

