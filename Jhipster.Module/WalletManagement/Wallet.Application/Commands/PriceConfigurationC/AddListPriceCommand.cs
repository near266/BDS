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
    public class AddListPriceCommand : IRequest<int>
    {
        public AddPriceDTO rq { get; set; }
    }
    public class AddListPriceCommandHandler : IRequestHandler<AddListPriceCommand, int>
    {
        private readonly ITypePriceRepository _repo;
        private readonly IMapper _mapper;

        public AddListPriceCommandHandler(ITypePriceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<int> Handle(AddListPriceCommand request, CancellationToken cancellationToken)
        {
            return await _repo.AddListPrice(request.rq, cancellationToken);
        }
    }
}
