using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;

namespace Wallet.Application.Commands.TypePriceC
{
    public class DeleteTypePriceCommand : IRequest<int>
    {
        public List<Guid> ListId { get; set; }
    }
    public class DeleteTypePriceCommandHandler : IRequestHandler<DeleteTypePriceCommand, int>
    {
        private readonly ITypePriceRepository _repo;

        public DeleteTypePriceCommandHandler(ITypePriceRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Handle(DeleteTypePriceCommand request, CancellationToken cancellationToken)
        {
            var res = await _repo.Delete(request.ListId, cancellationToken);
            return res;
        }
    }
}
