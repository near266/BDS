using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;

namespace Wallet.Application.Queries.WalletsQ
{
    public class GetWalletByUserIdQuery : IRequest<WalletResponseDTO>
    {
        public string? UserId { get; set; }
    }
    public class GetWalletByUserIdQueryHandler : IRequestHandler<GetWalletByUserIdQuery, WalletResponseDTO>
    {
        private readonly IWalletRepository _walletRepository;
        public GetWalletByUserIdQueryHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }
        public async Task<WalletResponseDTO> Handle(GetWalletByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _walletRepository.GetWalletByUserId(request.UserId);
        }
    }   
}
