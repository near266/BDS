using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.DTO;
using Wallet.Application.Persistences;

namespace Wallet.Application.Commands.PriceConfigurationC
{
    public class UpdateListPriceCommand : IRequest<int>
    {
        public UpdatePriceDTO rq { get; set; }
    }
    public class UpdateListPriceCommandHandler : IRequestHandler<UpdateListPriceCommand, int>
    {
        private readonly ITypePriceRepository _repo;
        private readonly IMapper _mapper;

        public UpdateListPriceCommandHandler(ITypePriceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateListPriceCommand request, CancellationToken cancellationToken)
        {
            return await _repo.UpdateListPrice(request.rq, cancellationToken);
        }
    }
}
