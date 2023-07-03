using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wallet.Application.Persistences;
using Wallet.Domain.Entities;

namespace Wallet.Application.Commands.TermConditionConfigurationC
{
    public class UpdateTermConditionConfigurationCommand : IRequest<int>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public Guid? TypeTermId { get; set; }
        [JsonIgnore]
        public string? LastModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? LastModifiedDate { get; set; }
        public class UpdateTermConditionConfigurationCommandHandler : IRequestHandler<UpdateTermConditionConfigurationCommand,int>
        {
            private readonly ITermConditionConfigurationRepository _repo;
            private readonly IMapper _mapper;

            public UpdateTermConditionConfigurationCommandHandler(ITermConditionConfigurationRepository repo,IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }

            public async Task<int> Handle(UpdateTermConditionConfigurationCommand request, CancellationToken cancellationToken)
            {
                var map = _mapper.Map<TermConditionConfiguration>(request);
                return await _repo.Update(map, cancellationToken);
            }
        }
    }
}
