using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;

namespace Wallet.Application.Commands.PriceConfigurationC
{
    public class DeletePriceConfigurationCommand : IRequest<int>
    {
        public List<Guid> ListId { get; set; }
    }
    public class DeletePriceConfigurationCommandHandler : IRequestHandler<DeletePriceConfigurationCommand, int>
    {
        private readonly IPriceConfigurationRepository _repo;
        private readonly IMapper _mapper;

        public DeletePriceConfigurationCommandHandler(IPriceConfigurationRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeletePriceConfigurationCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.ListId, cancellationToken);
        }
    }
}
