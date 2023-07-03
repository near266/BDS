using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;

namespace Wallet.Application.Commands.TermConditionConfigurationC
{
    public class DeleteTermConditionConfigurationCommand : IRequest<int>
    {
        public List<Guid> ListId { get; set; }
    }
    public class DeleteTermConditionConfigurationCommandHandler : IRequestHandler<DeleteTermConditionConfigurationCommand, int>
    {
        private readonly ITermConditionConfigurationRepository _repo;
        private readonly IMapper _mapper;

        public DeleteTermConditionConfigurationCommandHandler(ITermConditionConfigurationRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;   
        }

        public async Task<int> Handle(DeleteTermConditionConfigurationCommand request, CancellationToken cancellationToken)
        {
            return await _repo.Delete(request.ListId, cancellationToken);
        }
    }
}
