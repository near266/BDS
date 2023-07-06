using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.TermConditionConfigurationC
{
    public class AddTypeTermCommand :IRequest<int>
    {
        public string Name { get; set; }
        public string? DetailTerm { get; set; }
    }
    public class AddTypeTermCommandHandler : IRequestHandler<AddTypeTermCommand, int>
    {
        private readonly ITypeTermRepository _repo;
        private readonly IMapper _mapper;

        public AddTypeTermCommandHandler(ITypeTermRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddTypeTermCommand request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<TypeTerm>(request);
            return await _repo.AddTypeTerm(map, cancellationToken);
        }
    }
}
