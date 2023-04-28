using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Commands.WalletsC;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.WalletsPromotionaC
{
    public class DeleteWalletPromotionalCommand:IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class DeleteWalletPromotionalCommandHandler : IRequestHandler<DeleteWalletPromotionalCommand, int>
    {
        private readonly IWalletPromotionalRepository _repo;
        private readonly IMapper _mapper;
        public DeleteWalletPromotionalCommandHandler(IWalletPromotionalRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteWalletPromotionalCommand rq, CancellationToken cancellationToken)
        {
            return await _repo.Delete(rq.Id);
        }
    }
}
