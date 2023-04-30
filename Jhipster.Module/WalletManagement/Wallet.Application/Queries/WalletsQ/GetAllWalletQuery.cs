using AutoMapper;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Queries.WalletsQ
{
    public class GetAllWalletQuery :IRequest<IEnumerable<WalletEntity>>
    { 
    }
    public class GetAllQueryHandler : IRequestHandler<GetAllWalletQuery, IEnumerable<WalletEntity>>
    {
        private readonly IWalletRepository _repo;
        private readonly IMapper _mapper;
        public GetAllQueryHandler(IWalletRepository repository,IMapper mapper) 
        {
        
            _repo = repository;
            _mapper = mapper;
        }
     public async Task<IEnumerable<WalletEntity>> Handle (GetAllWalletQuery request , CancellationToken cancellationToken)
        {
            return await _repo.GetAll();
        }
    }
}
