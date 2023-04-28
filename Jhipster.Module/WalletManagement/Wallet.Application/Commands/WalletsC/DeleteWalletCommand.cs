using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.WalletsC
{
    public class DeleteWalletCommand :IRequest<int>
    {
        public Guid Id { get; set; }
    }
    public class DeleteWalletCommandHandler : IRequestHandler<DeleteWalletCommand, int>
    {
        private readonly IWalletRepository _repo;
        private readonly IMapper _mapper;
        public DeleteWalletCommandHandler(IWalletRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteWalletCommand rq, CancellationToken cancellationToken)
        {
            return await _repo.Delete(rq.Id);
        }
    }
}
